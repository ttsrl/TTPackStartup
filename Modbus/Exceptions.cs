using System;
using System.Runtime.Serialization;

namespace Modbus.Exceptions
{
    /// <summary>
    /// Exception to be thrown if serial port is not opened
    /// </summary>
    class SerialPortNotOpenedException : ModbusException
    {
        public SerialPortNotOpenedException()
         : base()
        {
        }

        public SerialPortNotOpenedException(string message)
            : base(message)
        {
        }

        public SerialPortNotOpenedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected SerialPortNotOpenedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// Exception to be thrown if Connection to Modbus device failed
    /// </summary>
    class ConnectionException : ModbusException
    {
        public ConnectionException()
         : base()
        {
        }

        public ConnectionException(string message)
            : base(message)
        {
        }

        public ConnectionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ConnectionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// Exception to be thrown if Modbus Server returns error code "Function code not supported"
    /// </summary>
    class FunctionCodeNotSupportedException : ModbusException
    {
        public FunctionCodeNotSupportedException()
         : base()
        {
        }

        public FunctionCodeNotSupportedException(string message)
            : base(message)
        {
        }

        public FunctionCodeNotSupportedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected FunctionCodeNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// Exception to be thrown if Modbus Server returns error code "quantity invalid"
    /// </summary>
    class QuantityInvalidException : ModbusException
    {
        public QuantityInvalidException()
         : base()
        {
        }

        public QuantityInvalidException(string message)
            : base(message)
        {
        }

        public QuantityInvalidException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected QuantityInvalidException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// Exception to be thrown if Modbus Server returns error code "starting adddress and quantity invalid"
    /// </summary>
    class StartingAddressInvalidException : ModbusException
    {
        public StartingAddressInvalidException()
         : base()
        {
        }

        public StartingAddressInvalidException(string message)
            : base(message)
        {
        }

        public StartingAddressInvalidException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected StartingAddressInvalidException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// Exception to be thrown if Modbus Server returns error code "Function Code not executed (0x04)"
    /// </summary>
    class ModbusException : Exception
    {
        public ModbusException()
         : base()
        {
        }

        public ModbusException(string message)
            : base(message)
        {
        }

        public ModbusException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ModbusException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// Exception to be thrown if CRC Check failed
    /// </summary>
    class CRCCheckFailedException : ModbusException
    {
        public CRCCheckFailedException()
         : base()
        {
        }

        public CRCCheckFailedException(string message)
            : base(message)
        {
        }

        public CRCCheckFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CRCCheckFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

}
