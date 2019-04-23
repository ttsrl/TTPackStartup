using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using TouchPanels.Devices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Automation.Provider;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using System.Reflection;

namespace TTPackStartup
{
    sealed partial class App : Application
    {
        const string CalibrationFilename = "TSC2046";
        private Tsc2046 tsc2046;
        private TouchPanels.TouchProcessor processor;
        private Point lastPosition = new Point(double.NaN, double.NaN);

        /// <summary>
        /// Inizializza l'oggetto Application singleton. Si tratta della prima riga del codice creato
        /// creato e, come tale, corrisponde all'equivalente logico di main() o WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Richiamato quando l'applicazione viene avviata normalmente dall'utente. All'avvio dell'applicazione
        /// verranno usati altri punti di ingresso per aprire un file specifico.
        /// </summary>
        /// <param name="e">Dettagli sulla richiesta e sul processo di avvio.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Non ripetere l'inizializzazione dell'applicazione se la finestra già dispone di contenuto,
            // assicurarsi solo che la finestra sia attiva
            if (rootFrame == null)
            {
                // Creare un frame che agisca da contesto di navigazione e passare alla prima pagina
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: caricare lo stato dall'applicazione sospesa in precedenza
                }

                // Posizionare il frame nella finestra corrente
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // Quando lo stack di esplorazione non viene ripristinato, passare alla prima pagina
                    // e configurare la nuova pagina passando le informazioni richieste come parametro
                    // parametro
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Assicurarsi che la finestra corrente sia attiva
                Window.Current.Activate();
                InitTouch();
            }
        }

        /// <summary>
        /// Chiamato quando la navigazione a una determinata pagina ha esito negativo
        /// </summary>
        /// <param name="sender">Frame la cui navigazione non è riuscita</param>
        /// <param name="e">Dettagli sull'errore di navigazione.</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Richiamato quando l'esecuzione dell'applicazione viene sospesa. Lo stato dell'applicazione viene salvato
        /// senza che sia noto se l'applicazione verrà terminata o ripresa con il contenuto
        /// della memoria ancora integro.
        /// </summary>
        /// <param name="sender">Origine della richiesta di sospensione.</param>
        /// <param name="e">Dettagli relativi alla richiesta di sospensione.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: salvare lo stato dell'applicazione e arrestare eventuali attività eseguite in background
            deferral.Complete();
        }

        private async void InitTouch()
        {
            tsc2046 = await TouchPanels.Devices.Tsc2046.GetDefaultAsync();
            try
            {
                await tsc2046.LoadCalibrationAsync(CalibrationFilename);
            }
            catch (System.IO.FileNotFoundException)
            {
                await CalibrateTouch(); //Initiate calibration if we don't have a calibration on file
            }
            catch (System.UnauthorizedAccessException)
            {
                //No access to documents folder
                await new Windows.UI.Popups.MessageDialog("Non è stato possibile avviare il file di configurazione del touchscreen", "Errore").ShowAsync();
                throw;
            }
            //Load up the touch processor and listen for touch events
            processor = new TouchPanels.TouchProcessor(tsc2046);
            processor.PointerDown += Processor_PointerDown;
            processor.PointerMoved += Processor_PointerMoved;
            processor.PointerUp += Processor_PointerUp;
        }

        IScrollProvider currentScrollItem;
        object currentObjClick;

        private void Processor_PointerDown(object sender, TouchPanels.PointerEventArgs e)
        {
            var obj = FindElement(e.Position);
            currentScrollItem = obj as IScrollProvider;
            if (obj != null)
            {
                if (obj.GetType() == typeof(ButtonAutomationPeer))
                {
                    var button = (Button)((ButtonAutomationPeer)obj).Owner;
                    button.Background = new SolidColorBrush(Color.FromArgb(255, 40, 159, 255));
                }
                currentObjClick = obj;
            }
            lastPosition = e.Position;
        }
        private void Processor_PointerMoved(object sender, TouchPanels.PointerEventArgs e)
        {
            if (currentScrollItem != null)
            {
                double dx = e.Position.X - lastPosition.X;
                double dy = e.Position.Y - lastPosition.Y;
                if (!currentScrollItem.HorizontallyScrollable) dx = 0;
                if (!currentScrollItem.VerticallyScrollable) dy = 0;

                Windows.UI.Xaml.Automation.ScrollAmount h = Windows.UI.Xaml.Automation.ScrollAmount.NoAmount;
                Windows.UI.Xaml.Automation.ScrollAmount v = Windows.UI.Xaml.Automation.ScrollAmount.NoAmount;
                if (dx < 0) h = Windows.UI.Xaml.Automation.ScrollAmount.SmallIncrement;
                else if (dx > 0) h = Windows.UI.Xaml.Automation.ScrollAmount.SmallDecrement;
                if (dy < 0) v = Windows.UI.Xaml.Automation.ScrollAmount.SmallIncrement;
                else if (dy > 0) v = Windows.UI.Xaml.Automation.ScrollAmount.SmallDecrement;
                currentScrollItem.Scroll(h, v);
            }
            lastPosition = e.Position;
        }
        private void Processor_PointerUp(object sender, TouchPanels.PointerEventArgs e)
        {
            var obj = FindElement(e.Position);
            if (currentObjClick != null)
            {
                if (currentObjClick.GetType() == typeof(ButtonAutomationPeer))
                {
                    var button = (Button)((ButtonAutomationPeer)currentObjClick).Owner;
                    button.Background = new SolidColorBrush(Color.FromArgb(255, 160, 160, 160));
                }

                //se ho completato il click sopra lo stesso oggettto
                if (obj != null && currentObjClick.GetHashCode() == obj.GetHashCode())
                {
                    var p = currentObjClick as IInvokeProvider;
                    p?.Invoke();
                }
            }
            currentScrollItem = null;
        }

        private bool _isCalibrating = false; //flag used to ignore the touch processor while calibrating
        private async Task CalibrateTouch()
        {
            _isCalibrating = true;
            var calibration = await TouchPanels.UI.LcdCalibrationView.CalibrateScreenAsync(tsc2046);
            _isCalibrating = false;
            tsc2046.SetCalibration(calibration.A, calibration.B, calibration.C, calibration.D, calibration.E, calibration.F);
            try
            {
                await tsc2046.SaveCalibrationAsync(CalibrationFilename);
            }
            catch (Exception ex)
            {
                await new Windows.UI.Popups.MessageDialog("Non è stato possibile avviare la calibrazione del touchscreen", "Errore").ShowAsync();
            }
        }

        private IScrollProvider FindElementToScroll(Point screenPosition)
        {
            if (_isCalibrating) return null;

            var elements = VisualTreeHelper.FindElementsInHostCoordinates(new Windows.Foundation.Point(screenPosition.X, screenPosition.Y), Window.Current.Content, false);
            //Search for buttons in the visual tree that we can invoke
            //If we can find an element button that implements the 'Invoke' automation pattern (usually buttons), we'll invoke it
            foreach (var e in elements.OfType<FrameworkElement>())
            {
                var element = e;
                AutomationPeer peer = null;
                object pattern = null;
                while (true)
                {
                    peer = FrameworkElementAutomationPeer.FromElement(element);
                    if (peer != null)
                    {
                        pattern = peer.GetPattern(PatternInterface.Invoke);
                        if (pattern != null)
                        {
                            break;
                        }
                        pattern = peer.GetPattern(PatternInterface.Scroll);
                        if (pattern != null)
                        {
                            break;
                        }
                    }
                    var parent = VisualTreeHelper.GetParent(element);
                    if (parent is FrameworkElement)
                        element = parent as FrameworkElement;
                    else
                        break;
                }
                if (pattern != null)
                {
                    //var p = pattern as IInvokeProvider;
                    //p?.Invoke();
                    return pattern as IScrollProvider;
                }
            }
            return null;
        }

        private object FindElement(Point screenPosition)
        {
            if (_isCalibrating) return null;
            var elements = VisualTreeHelper.FindElementsInHostCoordinates(new Windows.Foundation.Point(screenPosition.X, screenPosition.Y), Window.Current.Content, false);
            foreach (var e in elements.OfType<FrameworkElement>())
            {
                var element = e;
                AutomationPeer peer = null;
                object pattern = null;
                while (true)
                {
                    peer = FrameworkElementAutomationPeer.FromElement(element);
                    if (peer != null)
                    {
                        pattern = peer.GetPattern(PatternInterface.Invoke);
                        if (pattern != null)
                        {
                            break;
                        }
                        pattern = peer.GetPattern(PatternInterface.Scroll);
                        if (pattern != null)
                        {
                            break;
                        }
                    }
                    var parent = VisualTreeHelper.GetParent(element);
                    if (parent is FrameworkElement)
                        element = parent as FrameworkElement;
                    else
                        break;
                }
                if (pattern != null)
                {
                    return pattern;
                }
            }
            return null;
        }
    }
}