using Bonsai;
using Bonsai.Harp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Xml.Serialization;

namespace Harp.RgbArray
{
    /// <summary>
    /// Generates events and processes commands for the RgbArray device connected
    /// at the specified serial port.
    /// </summary>
    [Combinator(MethodName = nameof(Generate))]
    [WorkflowElementCategory(ElementCategory.Source)]
    [Description("Generates events and processes commands for the RgbArray device.")]
    public partial class Device : Bonsai.Harp.Device, INamedElement
    {
        /// <summary>
        /// Represents the unique identity class of the <see cref="RgbArray"/> device.
        /// This field is constant.
        /// </summary>
        public const int WhoAmI = 1264;

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        public Device() : base(WhoAmI) { }

        string INamedElement.Name => nameof(RgbArray);

        /// <summary>
        /// Gets a read-only mapping from address to register type.
        /// </summary>
        public static new IReadOnlyDictionary<int, Type> RegisterMap { get; } = new Dictionary<int, Type>
            (Bonsai.Harp.Device.RegisterMap.ToDictionary(entry => entry.Key, entry => entry.Value))
        {
            { 32, typeof(LedStatus) },
            { 33, typeof(NumberOfLeds) },
            { 34, typeof(SetRgb) },
            { 35, typeof(SetRgbBus0) },
            { 36, typeof(SetRgbBus1) },
            { 37, typeof(SetRgbOff) },
            { 39, typeof(DI0Mode) },
            { 40, typeof(DO0Mode) },
            { 41, typeof(DO1Mode) },
            { 43, typeof(LatchOnNextUpdate) },
            { 44, typeof(DigitalInputState) },
            { 45, typeof(OutputSet) },
            { 46, typeof(OutputClear) },
            { 47, typeof(OutputToggle) },
            { 48, typeof(OutputState) },
            { 49, typeof(DigitalOutputPulsePeriod) },
            { 50, typeof(DigitalOutputPulseCount) },
            { 51, typeof(EventEnable) }
        };
    }

    /// <summary>
    /// Represents an operator that groups the sequence of <see cref="RgbArray"/>" messages by register type.
    /// </summary>
    [Description("Groups the sequence of RgbArray messages by register type.")]
    public partial class GroupByRegister : Combinator<HarpMessage, IGroupedObservable<Type, HarpMessage>>
    {
        /// <summary>
        /// Groups an observable sequence of <see cref="RgbArray"/> messages
        /// by register type.
        /// </summary>
        /// <param name="source">The sequence of Harp device messages.</param>
        /// <returns>
        /// A sequence of observable groups, each of which corresponds to a unique
        /// <see cref="RgbArray"/> register.
        /// </returns>
        public override IObservable<IGroupedObservable<Type, HarpMessage>> Process(IObservable<HarpMessage> source)
        {
            return source.GroupBy(message => Device.RegisterMap[message.Address]);
        }
    }

    /// <summary>
    /// Represents an operator that filters register-specific messages
    /// reported by the <see cref="RgbArray"/> device.
    /// </summary>
    /// <seealso cref="LedStatus"/>
    /// <seealso cref="NumberOfLeds"/>
    /// <seealso cref="SetRgb"/>
    /// <seealso cref="SetRgbBus0"/>
    /// <seealso cref="SetRgbBus1"/>
    /// <seealso cref="SetRgbOff"/>
    /// <seealso cref="DI0Mode"/>
    /// <seealso cref="DO0Mode"/>
    /// <seealso cref="DO1Mode"/>
    /// <seealso cref="LatchOnNextUpdate"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="OutputSet"/>
    /// <seealso cref="OutputClear"/>
    /// <seealso cref="OutputToggle"/>
    /// <seealso cref="OutputState"/>
    /// <seealso cref="DigitalOutputPulsePeriod"/>
    /// <seealso cref="DigitalOutputPulseCount"/>
    /// <seealso cref="EventEnable"/>
    [XmlInclude(typeof(LedStatus))]
    [XmlInclude(typeof(NumberOfLeds))]
    [XmlInclude(typeof(SetRgb))]
    [XmlInclude(typeof(SetRgbBus0))]
    [XmlInclude(typeof(SetRgbBus1))]
    [XmlInclude(typeof(SetRgbOff))]
    [XmlInclude(typeof(DI0Mode))]
    [XmlInclude(typeof(DO0Mode))]
    [XmlInclude(typeof(DO1Mode))]
    [XmlInclude(typeof(LatchOnNextUpdate))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(OutputSet))]
    [XmlInclude(typeof(OutputClear))]
    [XmlInclude(typeof(OutputToggle))]
    [XmlInclude(typeof(OutputState))]
    [XmlInclude(typeof(DigitalOutputPulsePeriod))]
    [XmlInclude(typeof(DigitalOutputPulseCount))]
    [XmlInclude(typeof(EventEnable))]
    [Description("Filters register-specific messages reported by the RgbArray device.")]
    public class FilterRegister : FilterRegisterBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterRegister"/> class.
        /// </summary>
        public FilterRegister()
        {
            Register = new LedStatus();
        }

        string INamedElement.Name
        {
            get => $"{nameof(RgbArray)}.{GetElementDisplayName(Register)}";
        }
    }

    /// <summary>
    /// Represents an operator which filters and selects specific messages
    /// reported by the RgbArray device.
    /// </summary>
    /// <seealso cref="LedStatus"/>
    /// <seealso cref="NumberOfLeds"/>
    /// <seealso cref="SetRgb"/>
    /// <seealso cref="SetRgbBus0"/>
    /// <seealso cref="SetRgbBus1"/>
    /// <seealso cref="SetRgbOff"/>
    /// <seealso cref="DI0Mode"/>
    /// <seealso cref="DO0Mode"/>
    /// <seealso cref="DO1Mode"/>
    /// <seealso cref="LatchOnNextUpdate"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="OutputSet"/>
    /// <seealso cref="OutputClear"/>
    /// <seealso cref="OutputToggle"/>
    /// <seealso cref="OutputState"/>
    /// <seealso cref="DigitalOutputPulsePeriod"/>
    /// <seealso cref="DigitalOutputPulseCount"/>
    /// <seealso cref="EventEnable"/>
    [XmlInclude(typeof(LedStatus))]
    [XmlInclude(typeof(NumberOfLeds))]
    [XmlInclude(typeof(SetRgb))]
    [XmlInclude(typeof(SetRgbBus0))]
    [XmlInclude(typeof(SetRgbBus1))]
    [XmlInclude(typeof(SetRgbOff))]
    [XmlInclude(typeof(DI0Mode))]
    [XmlInclude(typeof(DO0Mode))]
    [XmlInclude(typeof(DO1Mode))]
    [XmlInclude(typeof(LatchOnNextUpdate))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(OutputSet))]
    [XmlInclude(typeof(OutputClear))]
    [XmlInclude(typeof(OutputToggle))]
    [XmlInclude(typeof(OutputState))]
    [XmlInclude(typeof(DigitalOutputPulsePeriod))]
    [XmlInclude(typeof(DigitalOutputPulseCount))]
    [XmlInclude(typeof(EventEnable))]
    [XmlInclude(typeof(TimestampedLedStatus))]
    [XmlInclude(typeof(TimestampedNumberOfLeds))]
    [XmlInclude(typeof(TimestampedSetRgb))]
    [XmlInclude(typeof(TimestampedSetRgbBus0))]
    [XmlInclude(typeof(TimestampedSetRgbBus1))]
    [XmlInclude(typeof(TimestampedSetRgbOff))]
    [XmlInclude(typeof(TimestampedDI0Mode))]
    [XmlInclude(typeof(TimestampedDO0Mode))]
    [XmlInclude(typeof(TimestampedDO1Mode))]
    [XmlInclude(typeof(TimestampedLatchOnNextUpdate))]
    [XmlInclude(typeof(TimestampedDigitalInputState))]
    [XmlInclude(typeof(TimestampedOutputSet))]
    [XmlInclude(typeof(TimestampedOutputClear))]
    [XmlInclude(typeof(TimestampedOutputToggle))]
    [XmlInclude(typeof(TimestampedOutputState))]
    [XmlInclude(typeof(TimestampedDigitalOutputPulsePeriod))]
    [XmlInclude(typeof(TimestampedDigitalOutputPulseCount))]
    [XmlInclude(typeof(TimestampedEventEnable))]
    [Description("Filters and selects specific messages reported by the RgbArray device.")]
    public partial class Parse : ParseBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parse"/> class.
        /// </summary>
        public Parse()
        {
            Register = new LedStatus();
        }

        string INamedElement.Name => $"{nameof(RgbArray)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents an operator which formats a sequence of values as specific
    /// RgbArray register messages.
    /// </summary>
    /// <seealso cref="LedStatus"/>
    /// <seealso cref="NumberOfLeds"/>
    /// <seealso cref="SetRgb"/>
    /// <seealso cref="SetRgbBus0"/>
    /// <seealso cref="SetRgbBus1"/>
    /// <seealso cref="SetRgbOff"/>
    /// <seealso cref="DI0Mode"/>
    /// <seealso cref="DO0Mode"/>
    /// <seealso cref="DO1Mode"/>
    /// <seealso cref="LatchOnNextUpdate"/>
    /// <seealso cref="DigitalInputState"/>
    /// <seealso cref="OutputSet"/>
    /// <seealso cref="OutputClear"/>
    /// <seealso cref="OutputToggle"/>
    /// <seealso cref="OutputState"/>
    /// <seealso cref="DigitalOutputPulsePeriod"/>
    /// <seealso cref="DigitalOutputPulseCount"/>
    /// <seealso cref="EventEnable"/>
    [XmlInclude(typeof(LedStatus))]
    [XmlInclude(typeof(NumberOfLeds))]
    [XmlInclude(typeof(SetRgb))]
    [XmlInclude(typeof(SetRgbBus0))]
    [XmlInclude(typeof(SetRgbBus1))]
    [XmlInclude(typeof(SetRgbOff))]
    [XmlInclude(typeof(DI0Mode))]
    [XmlInclude(typeof(DO0Mode))]
    [XmlInclude(typeof(DO1Mode))]
    [XmlInclude(typeof(LatchOnNextUpdate))]
    [XmlInclude(typeof(DigitalInputState))]
    [XmlInclude(typeof(OutputSet))]
    [XmlInclude(typeof(OutputClear))]
    [XmlInclude(typeof(OutputToggle))]
    [XmlInclude(typeof(OutputState))]
    [XmlInclude(typeof(DigitalOutputPulsePeriod))]
    [XmlInclude(typeof(DigitalOutputPulseCount))]
    [XmlInclude(typeof(EventEnable))]
    [Description("Formats a sequence of values as specific RgbArray register messages.")]
    public partial class Format : FormatBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Format"/> class.
        /// </summary>
        public Format()
        {
            Register = new LedStatus();
        }

        string INamedElement.Name => $"{nameof(RgbArray)}.{GetElementDisplayName(Register)}";
    }

    /// <summary>
    /// Represents a register that manipulates messages from register LedStatus.
    /// </summary>
    [Description("")]
    public partial class LedStatus
    {
        /// <summary>
        /// Represents the address of the <see cref="LedStatus"/> register. This field is constant.
        /// </summary>
        public const int Address = 32;

        /// <summary>
        /// Represents the payload type of the <see cref="LedStatus"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="LedStatus"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LedStatus"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LedStatus"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LedStatus"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LedStatus"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LedStatus"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LedStatus"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LedStatus register.
    /// </summary>
    /// <seealso cref="LedStatus"/>
    [Description("Filters and selects timestamped messages from the LedStatus register.")]
    public partial class TimestampedLedStatus
    {
        /// <summary>
        /// Represents the address of the <see cref="LedStatus"/> register. This field is constant.
        /// </summary>
        public const int Address = LedStatus.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LedStatus"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return LedStatus.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the number of LEDs connected on each bus of the device.
    /// </summary>
    [Description("The number of LEDs connected on each bus of the device.")]
    public partial class NumberOfLeds
    {
        /// <summary>
        /// Represents the address of the <see cref="NumberOfLeds"/> register. This field is constant.
        /// </summary>
        public const int Address = 33;

        /// <summary>
        /// Represents the payload type of the <see cref="NumberOfLeds"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="NumberOfLeds"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="NumberOfLeds"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="NumberOfLeds"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="NumberOfLeds"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="NumberOfLeds"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="NumberOfLeds"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="NumberOfLeds"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// NumberOfLeds register.
    /// </summary>
    /// <seealso cref="NumberOfLeds"/>
    [Description("Filters and selects timestamped messages from the NumberOfLeds register.")]
    public partial class TimestampedNumberOfLeds
    {
        /// <summary>
        /// Represents the address of the <see cref="NumberOfLeds"/> register. This field is constant.
        /// </summary>
        public const int Address = NumberOfLeds.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="NumberOfLeds"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return NumberOfLeds.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.
    /// </summary>
    [Description("The RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
")]
    public partial class SetRgb
    {
        /// <summary>
        /// Represents the address of the <see cref="SetRgb"/> register. This field is constant.
        /// </summary>
        public const int Address = 34;

        /// <summary>
        /// Represents the payload type of the <see cref="SetRgb"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="SetRgb"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 192;

        /// <summary>
        /// Returns the payload data for <see cref="SetRgb"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte[] GetPayload(HarpMessage message)
        {
            return message.GetPayloadArray<byte>();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="SetRgb"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte[]> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadArray<byte>();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="SetRgb"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="SetRgb"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte[] value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="SetRgb"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="SetRgb"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte[] value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// SetRgb register.
    /// </summary>
    /// <seealso cref="SetRgb"/>
    [Description("Filters and selects timestamped messages from the SetRgb register.")]
    public partial class TimestampedSetRgb
    {
        /// <summary>
        /// Represents the address of the <see cref="SetRgb"/> register. This field is constant.
        /// </summary>
        public const int Address = SetRgb.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="SetRgb"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte[]> GetPayload(HarpMessage message)
        {
            return SetRgb.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.
    /// </summary>
    [Description("The RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
")]
    public partial class SetRgbBus0
    {
        /// <summary>
        /// Represents the address of the <see cref="SetRgbBus0"/> register. This field is constant.
        /// </summary>
        public const int Address = 35;

        /// <summary>
        /// Represents the payload type of the <see cref="SetRgbBus0"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="SetRgbBus0"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 96;

        /// <summary>
        /// Returns the payload data for <see cref="SetRgbBus0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte[] GetPayload(HarpMessage message)
        {
            return message.GetPayloadArray<byte>();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="SetRgbBus0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte[]> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadArray<byte>();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="SetRgbBus0"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="SetRgbBus0"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte[] value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="SetRgbBus0"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="SetRgbBus0"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte[] value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// SetRgbBus0 register.
    /// </summary>
    /// <seealso cref="SetRgbBus0"/>
    [Description("Filters and selects timestamped messages from the SetRgbBus0 register.")]
    public partial class TimestampedSetRgbBus0
    {
        /// <summary>
        /// Represents the address of the <see cref="SetRgbBus0"/> register. This field is constant.
        /// </summary>
        public const int Address = SetRgbBus0.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="SetRgbBus0"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte[]> GetPayload(HarpMessage message)
        {
            return SetRgbBus0.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.
    /// </summary>
    [Description("The RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
")]
    public partial class SetRgbBus1
    {
        /// <summary>
        /// Represents the address of the <see cref="SetRgbBus1"/> register. This field is constant.
        /// </summary>
        public const int Address = 36;

        /// <summary>
        /// Represents the payload type of the <see cref="SetRgbBus1"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="SetRgbBus1"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 96;

        /// <summary>
        /// Returns the payload data for <see cref="SetRgbBus1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte[] GetPayload(HarpMessage message)
        {
            return message.GetPayloadArray<byte>();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="SetRgbBus1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte[]> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadArray<byte>();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="SetRgbBus1"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="SetRgbBus1"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte[] value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="SetRgbBus1"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="SetRgbBus1"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte[] value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// SetRgbBus1 register.
    /// </summary>
    /// <seealso cref="SetRgbBus1"/>
    [Description("Filters and selects timestamped messages from the SetRgbBus1 register.")]
    public partial class TimestampedSetRgbBus1
    {
        /// <summary>
        /// Represents the address of the <see cref="SetRgbBus1"/> register. This field is constant.
        /// </summary>
        public const int Address = SetRgbBus1.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="SetRgbBus1"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte[]> GetPayload(HarpMessage message)
        {
            return SetRgbBus1.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the RGB color of the LEDs when they are off.
    /// </summary>
    [Description("The RGB color of the LEDs when they are off.")]
    public partial class SetRgbOff
    {
        /// <summary>
        /// Represents the address of the <see cref="SetRgbOff"/> register. This field is constant.
        /// </summary>
        public const int Address = 37;

        /// <summary>
        /// Represents the payload type of the <see cref="SetRgbOff"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="SetRgbOff"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 3;

        /// <summary>
        /// Returns the payload data for <see cref="SetRgbOff"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte[] GetPayload(HarpMessage message)
        {
            return message.GetPayloadArray<byte>();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="SetRgbOff"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte[]> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadArray<byte>();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="SetRgbOff"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="SetRgbOff"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte[] value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="SetRgbOff"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="SetRgbOff"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte[] value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// SetRgbOff register.
    /// </summary>
    /// <seealso cref="SetRgbOff"/>
    [Description("Filters and selects timestamped messages from the SetRgbOff register.")]
    public partial class TimestampedSetRgbOff
    {
        /// <summary>
        /// Represents the address of the <see cref="SetRgbOff"/> register. This field is constant.
        /// </summary>
        public const int Address = SetRgbOff.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="SetRgbOff"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte[]> GetPayload(HarpMessage message)
        {
            return SetRgbOff.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that manipulates messages from register DI0Mode.
    /// </summary>
    [Description("")]
    public partial class DI0Mode
    {
        /// <summary>
        /// Represents the address of the <see cref="DI0Mode"/> register. This field is constant.
        /// </summary>
        public const int Address = 39;

        /// <summary>
        /// Represents the payload type of the <see cref="DI0Mode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DI0Mode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DI0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DI0ModeConfig GetPayload(HarpMessage message)
        {
            return (DI0ModeConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DI0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DI0ModeConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DI0ModeConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DI0Mode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DI0Mode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DI0ModeConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DI0Mode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DI0Mode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DI0ModeConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DI0Mode register.
    /// </summary>
    /// <seealso cref="DI0Mode"/>
    [Description("Filters and selects timestamped messages from the DI0Mode register.")]
    public partial class TimestampedDI0Mode
    {
        /// <summary>
        /// Represents the address of the <see cref="DI0Mode"/> register. This field is constant.
        /// </summary>
        public const int Address = DI0Mode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DI0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DI0ModeConfig> GetPayload(HarpMessage message)
        {
            return DI0Mode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that manipulates messages from register DO0Mode.
    /// </summary>
    [Description("")]
    public partial class DO0Mode
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0Mode"/> register. This field is constant.
        /// </summary>
        public const int Address = 40;

        /// <summary>
        /// Represents the payload type of the <see cref="DO0Mode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO0Mode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DOModeConfig GetPayload(HarpMessage message)
        {
            return (DOModeConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DOModeConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DOModeConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO0Mode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0Mode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DOModeConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO0Mode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO0Mode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DOModeConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO0Mode register.
    /// </summary>
    /// <seealso cref="DO0Mode"/>
    [Description("Filters and selects timestamped messages from the DO0Mode register.")]
    public partial class TimestampedDO0Mode
    {
        /// <summary>
        /// Represents the address of the <see cref="DO0Mode"/> register. This field is constant.
        /// </summary>
        public const int Address = DO0Mode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO0Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DOModeConfig> GetPayload(HarpMessage message)
        {
            return DO0Mode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that manipulates messages from register DO1Mode.
    /// </summary>
    [Description("")]
    public partial class DO1Mode
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1Mode"/> register. This field is constant.
        /// </summary>
        public const int Address = 41;

        /// <summary>
        /// Represents the payload type of the <see cref="DO1Mode"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DO1Mode"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DO1Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DOModeConfig GetPayload(HarpMessage message)
        {
            return (DOModeConfig)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DO1Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DOModeConfig> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DOModeConfig)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DO1Mode"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1Mode"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DOModeConfig value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DO1Mode"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DO1Mode"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DOModeConfig value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DO1Mode register.
    /// </summary>
    /// <seealso cref="DO1Mode"/>
    [Description("Filters and selects timestamped messages from the DO1Mode register.")]
    public partial class TimestampedDO1Mode
    {
        /// <summary>
        /// Represents the address of the <see cref="DO1Mode"/> register. This field is constant.
        /// </summary>
        public const int Address = DO1Mode.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DO1Mode"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DOModeConfig> GetPayload(HarpMessage message)
        {
            return DO1Mode.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that latches the next update.
    /// </summary>
    [Description("Latches the next update.")]
    public partial class LatchOnNextUpdate
    {
        /// <summary>
        /// Represents the address of the <see cref="LatchOnNextUpdate"/> register. This field is constant.
        /// </summary>
        public const int Address = 43;

        /// <summary>
        /// Represents the payload type of the <see cref="LatchOnNextUpdate"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="LatchOnNextUpdate"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="LatchOnNextUpdate"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static EnableFlag GetPayload(HarpMessage message)
        {
            return (EnableFlag)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="LatchOnNextUpdate"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EnableFlag> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((EnableFlag)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="LatchOnNextUpdate"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LatchOnNextUpdate"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, EnableFlag value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="LatchOnNextUpdate"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="LatchOnNextUpdate"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, EnableFlag value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// LatchOnNextUpdate register.
    /// </summary>
    /// <seealso cref="LatchOnNextUpdate"/>
    [Description("Filters and selects timestamped messages from the LatchOnNextUpdate register.")]
    public partial class TimestampedLatchOnNextUpdate
    {
        /// <summary>
        /// Represents the address of the <see cref="LatchOnNextUpdate"/> register. This field is constant.
        /// </summary>
        public const int Address = LatchOnNextUpdate.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="LatchOnNextUpdate"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<EnableFlag> GetPayload(HarpMessage message)
        {
            return LatchOnNextUpdate.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that reflects the state of DI digital lines of each Port.
    /// </summary>
    [Description("Reflects the state of DI digital lines of each Port")]
    public partial class DigitalInputState
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const int Address = 44;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalInputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalInputs GetPayload(HarpMessage message)
        {
            return (DigitalInputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalInputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalInputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalInputState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalInputState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalInputState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalInputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalInputState register.
    /// </summary>
    /// <seealso cref="DigitalInputState"/>
    [Description("Filters and selects timestamped messages from the DigitalInputState register.")]
    public partial class TimestampedDigitalInputState
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalInputState"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalInputState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalInputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalInputs> GetPayload(HarpMessage message)
        {
            return DigitalInputState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that set the specified digital output lines.
    /// </summary>
    [Description("Set the specified digital output lines.")]
    public partial class OutputSet
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const int Address = 45;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputSet"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputSet"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputSet"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputSet"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputSet register.
    /// </summary>
    /// <seealso cref="OutputSet"/>
    [Description("Filters and selects timestamped messages from the OutputSet register.")]
    public partial class TimestampedOutputSet
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputSet"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputSet.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputSet"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputSet.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that clear the specified digital output lines.
    /// </summary>
    [Description("Clear the specified digital output lines")]
    public partial class OutputClear
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const int Address = 46;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputClear"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputClear"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputClear"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputClear"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputClear register.
    /// </summary>
    /// <seealso cref="OutputClear"/>
    [Description("Filters and selects timestamped messages from the OutputClear register.")]
    public partial class TimestampedOutputClear
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputClear"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputClear.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputClear"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputClear.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that toggle the specified digital output lines.
    /// </summary>
    [Description("Toggle the specified digital output lines")]
    public partial class OutputToggle
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputToggle"/> register. This field is constant.
        /// </summary>
        public const int Address = 47;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputToggle"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="OutputToggle"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputToggle"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputToggle"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputToggle"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputToggle"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputToggle register.
    /// </summary>
    /// <seealso cref="OutputToggle"/>
    [Description("Filters and selects timestamped messages from the OutputToggle register.")]
    public partial class TimestampedOutputToggle
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputToggle"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputToggle.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputToggle"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputToggle.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that write the state of all digital output lines.
    /// </summary>
    [Description("Write the state of all digital output lines")]
    public partial class OutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = 48;

        /// <summary>
        /// Represents the payload type of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="OutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static DigitalOutputs GetPayload(HarpMessage message)
        {
            return (DigitalOutputs)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="OutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((DigitalOutputs)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="OutputState"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputState"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="OutputState"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="OutputState"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, DigitalOutputs value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// OutputState register.
    /// </summary>
    /// <seealso cref="OutputState"/>
    [Description("Filters and selects timestamped messages from the OutputState register.")]
    public partial class TimestampedOutputState
    {
        /// <summary>
        /// Represents the address of the <see cref="OutputState"/> register. This field is constant.
        /// </summary>
        public const int Address = OutputState.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="OutputState"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<DigitalOutputs> GetPayload(HarpMessage message)
        {
            return OutputState.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that the pulse period in milliseconds for digital outputs.
    /// </summary>
    [Description("The pulse period in milliseconds for digital outputs.")]
    public partial class DigitalOutputPulsePeriod
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputPulsePeriod"/> register. This field is constant.
        /// </summary>
        public const int Address = 49;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalOutputPulsePeriod"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U16;

        /// <summary>
        /// Represents the length of the <see cref="DigitalOutputPulsePeriod"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalOutputPulsePeriod"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static ushort GetPayload(HarpMessage message)
        {
            return message.GetPayloadUInt16();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalOutputPulsePeriod"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadUInt16();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalOutputPulsePeriod"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputPulsePeriod"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalOutputPulsePeriod"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputPulsePeriod"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, ushort value)
        {
            return HarpMessage.FromUInt16(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalOutputPulsePeriod register.
    /// </summary>
    /// <seealso cref="DigitalOutputPulsePeriod"/>
    [Description("Filters and selects timestamped messages from the DigitalOutputPulsePeriod register.")]
    public partial class TimestampedDigitalOutputPulsePeriod
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputPulsePeriod"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalOutputPulsePeriod.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalOutputPulsePeriod"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<ushort> GetPayload(HarpMessage message)
        {
            return DigitalOutputPulsePeriod.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that triggers the specified number of pulses on the digital output lines.
    /// </summary>
    [Description("Triggers the specified number of pulses on the digital output lines.")]
    public partial class DigitalOutputPulseCount
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputPulseCount"/> register. This field is constant.
        /// </summary>
        public const int Address = 50;

        /// <summary>
        /// Represents the payload type of the <see cref="DigitalOutputPulseCount"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="DigitalOutputPulseCount"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="DigitalOutputPulseCount"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static byte GetPayload(HarpMessage message)
        {
            return message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="DigitalOutputPulseCount"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetTimestampedPayload(HarpMessage message)
        {
            return message.GetTimestampedPayloadByte();
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="DigitalOutputPulseCount"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputPulseCount"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, messageType, value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="DigitalOutputPulseCount"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="DigitalOutputPulseCount"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, byte value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// DigitalOutputPulseCount register.
    /// </summary>
    /// <seealso cref="DigitalOutputPulseCount"/>
    [Description("Filters and selects timestamped messages from the DigitalOutputPulseCount register.")]
    public partial class TimestampedDigitalOutputPulseCount
    {
        /// <summary>
        /// Represents the address of the <see cref="DigitalOutputPulseCount"/> register. This field is constant.
        /// </summary>
        public const int Address = DigitalOutputPulseCount.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="DigitalOutputPulseCount"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<byte> GetPayload(HarpMessage message)
        {
            return DigitalOutputPulseCount.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents a register that specifies the active events in the device.
    /// </summary>
    [Description("Specifies the active events in the device.")]
    public partial class EventEnable
    {
        /// <summary>
        /// Represents the address of the <see cref="EventEnable"/> register. This field is constant.
        /// </summary>
        public const int Address = 51;

        /// <summary>
        /// Represents the payload type of the <see cref="EventEnable"/> register. This field is constant.
        /// </summary>
        public const PayloadType RegisterType = PayloadType.U8;

        /// <summary>
        /// Represents the length of the <see cref="EventEnable"/> register. This field is constant.
        /// </summary>
        public const int RegisterLength = 1;

        /// <summary>
        /// Returns the payload data for <see cref="EventEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the message payload.</returns>
        public static RgbArrayEvents GetPayload(HarpMessage message)
        {
            return (RgbArrayEvents)message.GetPayloadByte();
        }

        /// <summary>
        /// Returns the timestamped payload data for <see cref="EventEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<RgbArrayEvents> GetTimestampedPayload(HarpMessage message)
        {
            var payload = message.GetTimestampedPayloadByte();
            return Timestamped.Create((RgbArrayEvents)payload.Value, payload.Seconds);
        }

        /// <summary>
        /// Returns a Harp message for the <see cref="EventEnable"/> register.
        /// </summary>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EventEnable"/> register
        /// with the specified message type and payload.
        /// </returns>
        public static HarpMessage FromPayload(MessageType messageType, RgbArrayEvents value)
        {
            return HarpMessage.FromByte(Address, messageType, (byte)value);
        }

        /// <summary>
        /// Returns a timestamped Harp message for the <see cref="EventEnable"/>
        /// register.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">The type of the Harp message.</param>
        /// <param name="value">The value to be stored in the message payload.</param>
        /// <returns>
        /// A <see cref="HarpMessage"/> object for the <see cref="EventEnable"/> register
        /// with the specified message type, timestamp, and payload.
        /// </returns>
        public static HarpMessage FromPayload(double timestamp, MessageType messageType, RgbArrayEvents value)
        {
            return HarpMessage.FromByte(Address, timestamp, messageType, (byte)value);
        }
    }

    /// <summary>
    /// Provides methods for manipulating timestamped messages from the
    /// EventEnable register.
    /// </summary>
    /// <seealso cref="EventEnable"/>
    [Description("Filters and selects timestamped messages from the EventEnable register.")]
    public partial class TimestampedEventEnable
    {
        /// <summary>
        /// Represents the address of the <see cref="EventEnable"/> register. This field is constant.
        /// </summary>
        public const int Address = EventEnable.Address;

        /// <summary>
        /// Returns timestamped payload data for <see cref="EventEnable"/> register messages.
        /// </summary>
        /// <param name="message">A <see cref="HarpMessage"/> object representing the register message.</param>
        /// <returns>A value representing the timestamped message payload.</returns>
        public static Timestamped<RgbArrayEvents> GetPayload(HarpMessage message)
        {
            return EventEnable.GetTimestampedPayload(message);
        }
    }

    /// <summary>
    /// Represents an operator which creates standard message payloads for the
    /// RgbArray device.
    /// </summary>
    /// <seealso cref="CreateLedStatusPayload"/>
    /// <seealso cref="CreateNumberOfLedsPayload"/>
    /// <seealso cref="CreateSetRgbPayload"/>
    /// <seealso cref="CreateSetRgbBus0Payload"/>
    /// <seealso cref="CreateSetRgbBus1Payload"/>
    /// <seealso cref="CreateSetRgbOffPayload"/>
    /// <seealso cref="CreateDI0ModePayload"/>
    /// <seealso cref="CreateDO0ModePayload"/>
    /// <seealso cref="CreateDO1ModePayload"/>
    /// <seealso cref="CreateLatchOnNextUpdatePayload"/>
    /// <seealso cref="CreateDigitalInputStatePayload"/>
    /// <seealso cref="CreateOutputSetPayload"/>
    /// <seealso cref="CreateOutputClearPayload"/>
    /// <seealso cref="CreateOutputTogglePayload"/>
    /// <seealso cref="CreateOutputStatePayload"/>
    /// <seealso cref="CreateDigitalOutputPulsePeriodPayload"/>
    /// <seealso cref="CreateDigitalOutputPulseCountPayload"/>
    /// <seealso cref="CreateEventEnablePayload"/>
    [XmlInclude(typeof(CreateLedStatusPayload))]
    [XmlInclude(typeof(CreateNumberOfLedsPayload))]
    [XmlInclude(typeof(CreateSetRgbPayload))]
    [XmlInclude(typeof(CreateSetRgbBus0Payload))]
    [XmlInclude(typeof(CreateSetRgbBus1Payload))]
    [XmlInclude(typeof(CreateSetRgbOffPayload))]
    [XmlInclude(typeof(CreateDI0ModePayload))]
    [XmlInclude(typeof(CreateDO0ModePayload))]
    [XmlInclude(typeof(CreateDO1ModePayload))]
    [XmlInclude(typeof(CreateLatchOnNextUpdatePayload))]
    [XmlInclude(typeof(CreateDigitalInputStatePayload))]
    [XmlInclude(typeof(CreateOutputSetPayload))]
    [XmlInclude(typeof(CreateOutputClearPayload))]
    [XmlInclude(typeof(CreateOutputTogglePayload))]
    [XmlInclude(typeof(CreateOutputStatePayload))]
    [XmlInclude(typeof(CreateDigitalOutputPulsePeriodPayload))]
    [XmlInclude(typeof(CreateDigitalOutputPulseCountPayload))]
    [XmlInclude(typeof(CreateEventEnablePayload))]
    [XmlInclude(typeof(CreateTimestampedLedStatusPayload))]
    [XmlInclude(typeof(CreateTimestampedNumberOfLedsPayload))]
    [XmlInclude(typeof(CreateTimestampedSetRgbPayload))]
    [XmlInclude(typeof(CreateTimestampedSetRgbBus0Payload))]
    [XmlInclude(typeof(CreateTimestampedSetRgbBus1Payload))]
    [XmlInclude(typeof(CreateTimestampedSetRgbOffPayload))]
    [XmlInclude(typeof(CreateTimestampedDI0ModePayload))]
    [XmlInclude(typeof(CreateTimestampedDO0ModePayload))]
    [XmlInclude(typeof(CreateTimestampedDO1ModePayload))]
    [XmlInclude(typeof(CreateTimestampedLatchOnNextUpdatePayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalInputStatePayload))]
    [XmlInclude(typeof(CreateTimestampedOutputSetPayload))]
    [XmlInclude(typeof(CreateTimestampedOutputClearPayload))]
    [XmlInclude(typeof(CreateTimestampedOutputTogglePayload))]
    [XmlInclude(typeof(CreateTimestampedOutputStatePayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalOutputPulsePeriodPayload))]
    [XmlInclude(typeof(CreateTimestampedDigitalOutputPulseCountPayload))]
    [XmlInclude(typeof(CreateTimestampedEventEnablePayload))]
    [Description("Creates standard message payloads for the RgbArray device.")]
    public partial class CreateMessage : CreateMessageBuilder, INamedElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMessage"/> class.
        /// </summary>
        public CreateMessage()
        {
            Payload = new CreateLedStatusPayload();
        }

        string INamedElement.Name => $"{nameof(RgbArray)}.{GetElementDisplayName(Payload)}";
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// for register LedStatus.
    /// </summary>
    [DisplayName("LedStatusPayload")]
    [Description("Creates a message payload for register LedStatus.")]
    public partial class CreateLedStatusPayload
    {
        /// <summary>
        /// Gets or sets the value for register LedStatus.
        /// </summary>
        [Description("The value for register LedStatus.")]
        public byte LedStatus { get; set; }

        /// <summary>
        /// Creates a message payload for the LedStatus register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return LedStatus;
        }

        /// <summary>
        /// Creates a message for register LedStatus.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the LedStatus register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RgbArray.LedStatus.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// for register LedStatus.
    /// </summary>
    [DisplayName("TimestampedLedStatusPayload")]
    [Description("Creates a timestamped message payload for register LedStatus.")]
    public partial class CreateTimestampedLedStatusPayload : CreateLedStatusPayload
    {
        /// <summary>
        /// Creates a timestamped message for register LedStatus.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the LedStatus register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RgbArray.LedStatus.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that the number of LEDs connected on each bus of the device.
    /// </summary>
    [DisplayName("NumberOfLedsPayload")]
    [Description("Creates a message payload that the number of LEDs connected on each bus of the device.")]
    public partial class CreateNumberOfLedsPayload
    {
        /// <summary>
        /// Gets or sets the value that the number of LEDs connected on each bus of the device.
        /// </summary>
        [Range(min: long.MinValue, max: 32)]
        [Editor(DesignTypes.NumericUpDownEditor, DesignTypes.UITypeEditor)]
        [Description("The value that the number of LEDs connected on each bus of the device.")]
        public byte NumberOfLeds { get; set; }

        /// <summary>
        /// Creates a message payload for the NumberOfLeds register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return NumberOfLeds;
        }

        /// <summary>
        /// Creates a message that the number of LEDs connected on each bus of the device.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the NumberOfLeds register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RgbArray.NumberOfLeds.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that the number of LEDs connected on each bus of the device.
    /// </summary>
    [DisplayName("TimestampedNumberOfLedsPayload")]
    [Description("Creates a timestamped message payload that the number of LEDs connected on each bus of the device.")]
    public partial class CreateTimestampedNumberOfLedsPayload : CreateNumberOfLedsPayload
    {
        /// <summary>
        /// Creates a timestamped message that the number of LEDs connected on each bus of the device.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the NumberOfLeds register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RgbArray.NumberOfLeds.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.
    /// </summary>
    [DisplayName("SetRgbPayload")]
    [Description("Creates a message payload that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.")]
    public partial class CreateSetRgbPayload
    {
        /// <summary>
        /// Gets or sets the value that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.
        /// </summary>
        [Description("The value that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.")]
        public byte[] SetRgb { get; set; }

        /// <summary>
        /// Creates a message payload for the SetRgb register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte[] GetPayload()
        {
            return SetRgb;
        }

        /// <summary>
        /// Creates a message that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the SetRgb register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RgbArray.SetRgb.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.
    /// </summary>
    [DisplayName("TimestampedSetRgbPayload")]
    [Description("Creates a timestamped message payload that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.")]
    public partial class CreateTimestampedSetRgbPayload : CreateSetRgbPayload
    {
        /// <summary>
        /// Creates a timestamped message that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the SetRgb register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RgbArray.SetRgb.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.
    /// </summary>
    [DisplayName("SetRgbBus0Payload")]
    [Description("Creates a message payload that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.")]
    public partial class CreateSetRgbBus0Payload
    {
        /// <summary>
        /// Gets or sets the value that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.
        /// </summary>
        [Description("The value that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.")]
        public byte[] SetRgbBus0 { get; set; }

        /// <summary>
        /// Creates a message payload for the SetRgbBus0 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte[] GetPayload()
        {
            return SetRgbBus0;
        }

        /// <summary>
        /// Creates a message that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the SetRgbBus0 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RgbArray.SetRgbBus0.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.
    /// </summary>
    [DisplayName("TimestampedSetRgbBus0Payload")]
    [Description("Creates a timestamped message payload that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.")]
    public partial class CreateTimestampedSetRgbBus0Payload : CreateSetRgbBus0Payload
    {
        /// <summary>
        /// Creates a timestamped message that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the SetRgbBus0 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RgbArray.SetRgbBus0.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.
    /// </summary>
    [DisplayName("SetRgbBus1Payload")]
    [Description("Creates a message payload that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.")]
    public partial class CreateSetRgbBus1Payload
    {
        /// <summary>
        /// Gets or sets the value that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.
        /// </summary>
        [Description("The value that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.")]
        public byte[] SetRgbBus1 { get; set; }

        /// <summary>
        /// Creates a message payload for the SetRgbBus1 register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte[] GetPayload()
        {
            return SetRgbBus1;
        }

        /// <summary>
        /// Creates a message that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the SetRgbBus1 register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RgbArray.SetRgbBus1.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.
    /// </summary>
    [DisplayName("TimestampedSetRgbBus1Payload")]
    [Description("Creates a timestamped message payload that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.")]
    public partial class CreateTimestampedSetRgbBus1Payload : CreateSetRgbBus1Payload
    {
        /// <summary>
        /// Creates a timestamped message that the RGB color of each LED. The first 3 bytes are the RGB color of the first LED, the next 3 bytes are the RGB color of the second LED, and so on.
.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the SetRgbBus1 register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RgbArray.SetRgbBus1.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that the RGB color of the LEDs when they are off.
    /// </summary>
    [DisplayName("SetRgbOffPayload")]
    [Description("Creates a message payload that the RGB color of the LEDs when they are off.")]
    public partial class CreateSetRgbOffPayload
    {
        /// <summary>
        /// Gets or sets the value that the RGB color of the LEDs when they are off.
        /// </summary>
        [Description("The value that the RGB color of the LEDs when they are off.")]
        public byte[] SetRgbOff { get; set; }

        /// <summary>
        /// Creates a message payload for the SetRgbOff register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte[] GetPayload()
        {
            return SetRgbOff;
        }

        /// <summary>
        /// Creates a message that the RGB color of the LEDs when they are off.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the SetRgbOff register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RgbArray.SetRgbOff.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that the RGB color of the LEDs when they are off.
    /// </summary>
    [DisplayName("TimestampedSetRgbOffPayload")]
    [Description("Creates a timestamped message payload that the RGB color of the LEDs when they are off.")]
    public partial class CreateTimestampedSetRgbOffPayload : CreateSetRgbOffPayload
    {
        /// <summary>
        /// Creates a timestamped message that the RGB color of the LEDs when they are off.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the SetRgbOff register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RgbArray.SetRgbOff.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// for register DI0Mode.
    /// </summary>
    [DisplayName("DI0ModePayload")]
    [Description("Creates a message payload for register DI0Mode.")]
    public partial class CreateDI0ModePayload
    {
        /// <summary>
        /// Gets or sets the value for register DI0Mode.
        /// </summary>
        [Description("The value for register DI0Mode.")]
        public DI0ModeConfig DI0Mode { get; set; }

        /// <summary>
        /// Creates a message payload for the DI0Mode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DI0ModeConfig GetPayload()
        {
            return DI0Mode;
        }

        /// <summary>
        /// Creates a message for register DI0Mode.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DI0Mode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RgbArray.DI0Mode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// for register DI0Mode.
    /// </summary>
    [DisplayName("TimestampedDI0ModePayload")]
    [Description("Creates a timestamped message payload for register DI0Mode.")]
    public partial class CreateTimestampedDI0ModePayload : CreateDI0ModePayload
    {
        /// <summary>
        /// Creates a timestamped message for register DI0Mode.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DI0Mode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RgbArray.DI0Mode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// for register DO0Mode.
    /// </summary>
    [DisplayName("DO0ModePayload")]
    [Description("Creates a message payload for register DO0Mode.")]
    public partial class CreateDO0ModePayload
    {
        /// <summary>
        /// Gets or sets the value for register DO0Mode.
        /// </summary>
        [Description("The value for register DO0Mode.")]
        public DOModeConfig DO0Mode { get; set; }

        /// <summary>
        /// Creates a message payload for the DO0Mode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DOModeConfig GetPayload()
        {
            return DO0Mode;
        }

        /// <summary>
        /// Creates a message for register DO0Mode.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO0Mode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RgbArray.DO0Mode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// for register DO0Mode.
    /// </summary>
    [DisplayName("TimestampedDO0ModePayload")]
    [Description("Creates a timestamped message payload for register DO0Mode.")]
    public partial class CreateTimestampedDO0ModePayload : CreateDO0ModePayload
    {
        /// <summary>
        /// Creates a timestamped message for register DO0Mode.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO0Mode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RgbArray.DO0Mode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// for register DO1Mode.
    /// </summary>
    [DisplayName("DO1ModePayload")]
    [Description("Creates a message payload for register DO1Mode.")]
    public partial class CreateDO1ModePayload
    {
        /// <summary>
        /// Gets or sets the value for register DO1Mode.
        /// </summary>
        [Description("The value for register DO1Mode.")]
        public DOModeConfig DO1Mode { get; set; }

        /// <summary>
        /// Creates a message payload for the DO1Mode register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DOModeConfig GetPayload()
        {
            return DO1Mode;
        }

        /// <summary>
        /// Creates a message for register DO1Mode.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DO1Mode register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RgbArray.DO1Mode.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// for register DO1Mode.
    /// </summary>
    [DisplayName("TimestampedDO1ModePayload")]
    [Description("Creates a timestamped message payload for register DO1Mode.")]
    public partial class CreateTimestampedDO1ModePayload : CreateDO1ModePayload
    {
        /// <summary>
        /// Creates a timestamped message for register DO1Mode.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DO1Mode register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RgbArray.DO1Mode.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that latches the next update.
    /// </summary>
    [DisplayName("LatchOnNextUpdatePayload")]
    [Description("Creates a message payload that latches the next update.")]
    public partial class CreateLatchOnNextUpdatePayload
    {
        /// <summary>
        /// Gets or sets the value that latches the next update.
        /// </summary>
        [Description("The value that latches the next update.")]
        public EnableFlag LatchOnNextUpdate { get; set; }

        /// <summary>
        /// Creates a message payload for the LatchOnNextUpdate register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public EnableFlag GetPayload()
        {
            return LatchOnNextUpdate;
        }

        /// <summary>
        /// Creates a message that latches the next update.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the LatchOnNextUpdate register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RgbArray.LatchOnNextUpdate.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that latches the next update.
    /// </summary>
    [DisplayName("TimestampedLatchOnNextUpdatePayload")]
    [Description("Creates a timestamped message payload that latches the next update.")]
    public partial class CreateTimestampedLatchOnNextUpdatePayload : CreateLatchOnNextUpdatePayload
    {
        /// <summary>
        /// Creates a timestamped message that latches the next update.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the LatchOnNextUpdate register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RgbArray.LatchOnNextUpdate.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that reflects the state of DI digital lines of each Port.
    /// </summary>
    [DisplayName("DigitalInputStatePayload")]
    [Description("Creates a message payload that reflects the state of DI digital lines of each Port.")]
    public partial class CreateDigitalInputStatePayload
    {
        /// <summary>
        /// Gets or sets the value that reflects the state of DI digital lines of each Port.
        /// </summary>
        [Description("The value that reflects the state of DI digital lines of each Port.")]
        public DigitalInputs DigitalInputState { get; set; }

        /// <summary>
        /// Creates a message payload for the DigitalInputState register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalInputs GetPayload()
        {
            return DigitalInputState;
        }

        /// <summary>
        /// Creates a message that reflects the state of DI digital lines of each Port.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DigitalInputState register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RgbArray.DigitalInputState.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that reflects the state of DI digital lines of each Port.
    /// </summary>
    [DisplayName("TimestampedDigitalInputStatePayload")]
    [Description("Creates a timestamped message payload that reflects the state of DI digital lines of each Port.")]
    public partial class CreateTimestampedDigitalInputStatePayload : CreateDigitalInputStatePayload
    {
        /// <summary>
        /// Creates a timestamped message that reflects the state of DI digital lines of each Port.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DigitalInputState register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RgbArray.DigitalInputState.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that set the specified digital output lines.
    /// </summary>
    [DisplayName("OutputSetPayload")]
    [Description("Creates a message payload that set the specified digital output lines.")]
    public partial class CreateOutputSetPayload
    {
        /// <summary>
        /// Gets or sets the value that set the specified digital output lines.
        /// </summary>
        [Description("The value that set the specified digital output lines.")]
        public DigitalOutputs OutputSet { get; set; }

        /// <summary>
        /// Creates a message payload for the OutputSet register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return OutputSet;
        }

        /// <summary>
        /// Creates a message that set the specified digital output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OutputSet register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RgbArray.OutputSet.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that set the specified digital output lines.
    /// </summary>
    [DisplayName("TimestampedOutputSetPayload")]
    [Description("Creates a timestamped message payload that set the specified digital output lines.")]
    public partial class CreateTimestampedOutputSetPayload : CreateOutputSetPayload
    {
        /// <summary>
        /// Creates a timestamped message that set the specified digital output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OutputSet register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RgbArray.OutputSet.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that clear the specified digital output lines.
    /// </summary>
    [DisplayName("OutputClearPayload")]
    [Description("Creates a message payload that clear the specified digital output lines.")]
    public partial class CreateOutputClearPayload
    {
        /// <summary>
        /// Gets or sets the value that clear the specified digital output lines.
        /// </summary>
        [Description("The value that clear the specified digital output lines.")]
        public DigitalOutputs OutputClear { get; set; }

        /// <summary>
        /// Creates a message payload for the OutputClear register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return OutputClear;
        }

        /// <summary>
        /// Creates a message that clear the specified digital output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OutputClear register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RgbArray.OutputClear.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that clear the specified digital output lines.
    /// </summary>
    [DisplayName("TimestampedOutputClearPayload")]
    [Description("Creates a timestamped message payload that clear the specified digital output lines.")]
    public partial class CreateTimestampedOutputClearPayload : CreateOutputClearPayload
    {
        /// <summary>
        /// Creates a timestamped message that clear the specified digital output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OutputClear register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RgbArray.OutputClear.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that toggle the specified digital output lines.
    /// </summary>
    [DisplayName("OutputTogglePayload")]
    [Description("Creates a message payload that toggle the specified digital output lines.")]
    public partial class CreateOutputTogglePayload
    {
        /// <summary>
        /// Gets or sets the value that toggle the specified digital output lines.
        /// </summary>
        [Description("The value that toggle the specified digital output lines.")]
        public DigitalOutputs OutputToggle { get; set; }

        /// <summary>
        /// Creates a message payload for the OutputToggle register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return OutputToggle;
        }

        /// <summary>
        /// Creates a message that toggle the specified digital output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OutputToggle register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RgbArray.OutputToggle.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that toggle the specified digital output lines.
    /// </summary>
    [DisplayName("TimestampedOutputTogglePayload")]
    [Description("Creates a timestamped message payload that toggle the specified digital output lines.")]
    public partial class CreateTimestampedOutputTogglePayload : CreateOutputTogglePayload
    {
        /// <summary>
        /// Creates a timestamped message that toggle the specified digital output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OutputToggle register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RgbArray.OutputToggle.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that write the state of all digital output lines.
    /// </summary>
    [DisplayName("OutputStatePayload")]
    [Description("Creates a message payload that write the state of all digital output lines.")]
    public partial class CreateOutputStatePayload
    {
        /// <summary>
        /// Gets or sets the value that write the state of all digital output lines.
        /// </summary>
        [Description("The value that write the state of all digital output lines.")]
        public DigitalOutputs OutputState { get; set; }

        /// <summary>
        /// Creates a message payload for the OutputState register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public DigitalOutputs GetPayload()
        {
            return OutputState;
        }

        /// <summary>
        /// Creates a message that write the state of all digital output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the OutputState register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RgbArray.OutputState.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that write the state of all digital output lines.
    /// </summary>
    [DisplayName("TimestampedOutputStatePayload")]
    [Description("Creates a timestamped message payload that write the state of all digital output lines.")]
    public partial class CreateTimestampedOutputStatePayload : CreateOutputStatePayload
    {
        /// <summary>
        /// Creates a timestamped message that write the state of all digital output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the OutputState register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RgbArray.OutputState.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that the pulse period in milliseconds for digital outputs.
    /// </summary>
    [DisplayName("DigitalOutputPulsePeriodPayload")]
    [Description("Creates a message payload that the pulse period in milliseconds for digital outputs.")]
    public partial class CreateDigitalOutputPulsePeriodPayload
    {
        /// <summary>
        /// Gets or sets the value that the pulse period in milliseconds for digital outputs.
        /// </summary>
        [Description("The value that the pulse period in milliseconds for digital outputs.")]
        public ushort DigitalOutputPulsePeriod { get; set; }

        /// <summary>
        /// Creates a message payload for the DigitalOutputPulsePeriod register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public ushort GetPayload()
        {
            return DigitalOutputPulsePeriod;
        }

        /// <summary>
        /// Creates a message that the pulse period in milliseconds for digital outputs.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DigitalOutputPulsePeriod register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RgbArray.DigitalOutputPulsePeriod.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that the pulse period in milliseconds for digital outputs.
    /// </summary>
    [DisplayName("TimestampedDigitalOutputPulsePeriodPayload")]
    [Description("Creates a timestamped message payload that the pulse period in milliseconds for digital outputs.")]
    public partial class CreateTimestampedDigitalOutputPulsePeriodPayload : CreateDigitalOutputPulsePeriodPayload
    {
        /// <summary>
        /// Creates a timestamped message that the pulse period in milliseconds for digital outputs.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DigitalOutputPulsePeriod register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RgbArray.DigitalOutputPulsePeriod.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that triggers the specified number of pulses on the digital output lines.
    /// </summary>
    [DisplayName("DigitalOutputPulseCountPayload")]
    [Description("Creates a message payload that triggers the specified number of pulses on the digital output lines.")]
    public partial class CreateDigitalOutputPulseCountPayload
    {
        /// <summary>
        /// Gets or sets the value that triggers the specified number of pulses on the digital output lines.
        /// </summary>
        [Description("The value that triggers the specified number of pulses on the digital output lines.")]
        public byte DigitalOutputPulseCount { get; set; }

        /// <summary>
        /// Creates a message payload for the DigitalOutputPulseCount register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public byte GetPayload()
        {
            return DigitalOutputPulseCount;
        }

        /// <summary>
        /// Creates a message that triggers the specified number of pulses on the digital output lines.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the DigitalOutputPulseCount register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RgbArray.DigitalOutputPulseCount.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that triggers the specified number of pulses on the digital output lines.
    /// </summary>
    [DisplayName("TimestampedDigitalOutputPulseCountPayload")]
    [Description("Creates a timestamped message payload that triggers the specified number of pulses on the digital output lines.")]
    public partial class CreateTimestampedDigitalOutputPulseCountPayload : CreateDigitalOutputPulseCountPayload
    {
        /// <summary>
        /// Creates a timestamped message that triggers the specified number of pulses on the digital output lines.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the DigitalOutputPulseCount register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RgbArray.DigitalOutputPulseCount.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a message payload
    /// that specifies the active events in the device.
    /// </summary>
    [DisplayName("EventEnablePayload")]
    [Description("Creates a message payload that specifies the active events in the device.")]
    public partial class CreateEventEnablePayload
    {
        /// <summary>
        /// Gets or sets the value that specifies the active events in the device.
        /// </summary>
        [Description("The value that specifies the active events in the device.")]
        public RgbArrayEvents EventEnable { get; set; }

        /// <summary>
        /// Creates a message payload for the EventEnable register.
        /// </summary>
        /// <returns>The created message payload value.</returns>
        public RgbArrayEvents GetPayload()
        {
            return EventEnable;
        }

        /// <summary>
        /// Creates a message that specifies the active events in the device.
        /// </summary>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new message for the EventEnable register.</returns>
        public HarpMessage GetMessage(MessageType messageType)
        {
            return Harp.RgbArray.EventEnable.FromPayload(messageType, GetPayload());
        }
    }

    /// <summary>
    /// Represents an operator that creates a timestamped message payload
    /// that specifies the active events in the device.
    /// </summary>
    [DisplayName("TimestampedEventEnablePayload")]
    [Description("Creates a timestamped message payload that specifies the active events in the device.")]
    public partial class CreateTimestampedEventEnablePayload : CreateEventEnablePayload
    {
        /// <summary>
        /// Creates a timestamped message that specifies the active events in the device.
        /// </summary>
        /// <param name="timestamp">The timestamp of the message payload, in seconds.</param>
        /// <param name="messageType">Specifies the type of the created message.</param>
        /// <returns>A new timestamped message for the EventEnable register.</returns>
        public HarpMessage GetMessage(double timestamp, MessageType messageType)
        {
            return Harp.RgbArray.EventEnable.FromPayload(timestamp, messageType, GetPayload());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum DigitalInputs : byte
    {
        None = 0x0,
        DI0 = 0x1
    }

    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum DigitalOuputs : byte
    {
        None = 0x0,
        DO0 = 0x1,
        DO1 = 0x2,
        DO2 = 0x4,
        DO3 = 0x8,
        DO4 = 0x16
    }

    /// <summary>
    /// Specifies the operation mode of the DI0 pin.
    /// </summary>
    public enum DI0ModeConfig : byte
    {
        /// <summary>
        /// The DI0 pin functions as a passive digital input.
        /// </summary>
        None = 0,

        /// <summary>
        /// Update the LED colors when the DI0 pin transitions from low to high.
        /// </summary>
        UpdateOnRisingEdge = 1,

        /// <summary>
        /// Able to update RGBs when the pin is HIGH. Turn LEDs off when rising edge is detected.
        /// </summary>
        GateUpdate = 2
    }

    /// <summary>
    /// Specifies the operation mode of a Digital Output pin.
    /// </summary>
    public enum DOModeConfig : byte
    {
        /// <summary>
        /// The pin will function as a pure digital output.
        /// </summary>
        None = 0,

        /// <summary>
        /// A 1ms pulse will be triggered each time an RGB is updated.
        /// </summary>
        PulseOnUpdate = 1,

        /// <summary>
        /// A 1ms pulse will be triggered each time an new array is loaded RGB.
        /// </summary>
        PulseOnLoad = 2,

        /// <summary>
        /// The output pin will toggle each time an RGB is updated.
        /// </summary>
        ToggleOnUpdate = 3,

        /// <summary>
        /// The output pin will toggle each time an new array is loaded RGB.
        /// </summary>
        ToggleOnLoad = 4
    }

    /// <summary>
    /// Available events to be enable in the board.
    /// </summary>
    public enum RgbArrayEvents : byte
    {
        LedStatus = 1,
        DigitalInputs = 2
    }
}
