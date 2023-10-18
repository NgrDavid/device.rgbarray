using Bonsai.Harp;
using System.Threading;
using System.Threading.Tasks;

namespace Harp.RgbArray
{
    /// <inheritdoc/>
    public partial class Device
    {
        /// <summary>
        /// Initializes a new instance of the asynchronous API to configure and interface
        /// with RgbArray devices on the specified serial port.
        /// </summary>
        /// <param name="portName">
        /// The name of the serial port used to communicate with the Harp device.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous initialization operation. The value of
        /// the <see cref="Task{TResult}.Result"/> parameter contains a new instance of
        /// the <see cref="AsyncDevice"/> class.
        /// </returns>
        public static async Task<AsyncDevice> CreateAsync(string portName)
        {
            var device = new AsyncDevice(portName);
            var whoAmI = await device.ReadWhoAmIAsync();
            if (whoAmI != Device.WhoAmI)
            {
                var errorMessage = string.Format(
                    "The device ID {1} on {0} was unexpected. Check whether a RgbArray device is connected to the specified serial port.",
                    portName, whoAmI);
                throw new HarpException(errorMessage);
            }

            return device;
        }
    }

    /// <summary>
    /// Represents an asynchronous API to configure and interface with RgbArray devices.
    /// </summary>
    public partial class AsyncDevice : Bonsai.Harp.AsyncDevice
    {
        internal AsyncDevice(string portName)
            : base(portName)
        {
        }

        /// <summary>
        /// Asynchronously reads the contents of the LedStatus register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadLedStatusAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(LedStatus.Address), cancellationToken);
            return LedStatus.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LedStatus register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedLedStatusAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(LedStatus.Address), cancellationToken);
            return LedStatus.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LedStatus register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLedStatusAsync(byte value, CancellationToken cancellationToken = default)
        {
            var request = LedStatus.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LedCount register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadLedCountAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(LedCount.Address), cancellationToken);
            return LedCount.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LedCount register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedLedCountAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(LedCount.Address), cancellationToken);
            return LedCount.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LedCount register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLedCountAsync(byte value, CancellationToken cancellationToken = default)
        {
            var request = LedCount.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the RgbState register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte[]> ReadRgbStateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(RgbState.Address), cancellationToken);
            return RgbState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the RgbState register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte[]>> ReadTimestampedRgbStateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(RgbState.Address), cancellationToken);
            return RgbState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the RgbState register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteRgbStateAsync(byte[] value, CancellationToken cancellationToken = default)
        {
            var request = RgbState.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the RgbBus0State register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte[]> ReadRgbBus0StateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(RgbBus0State.Address), cancellationToken);
            return RgbBus0State.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the RgbBus0State register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte[]>> ReadTimestampedRgbBus0StateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(RgbBus0State.Address), cancellationToken);
            return RgbBus0State.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the RgbBus0State register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteRgbBus0StateAsync(byte[] value, CancellationToken cancellationToken = default)
        {
            var request = RgbBus0State.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the RgbBus1State register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte[]> ReadRgbBus1StateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(RgbBus1State.Address), cancellationToken);
            return RgbBus1State.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the RgbBus1State register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte[]>> ReadTimestampedRgbBus1StateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(RgbBus1State.Address), cancellationToken);
            return RgbBus1State.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the RgbBus1State register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteRgbBus1StateAsync(byte[] value, CancellationToken cancellationToken = default)
        {
            var request = RgbBus1State.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the RgbOffState register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte[]> ReadRgbOffStateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(RgbOffState.Address), cancellationToken);
            return RgbOffState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the RgbOffState register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte[]>> ReadTimestampedRgbOffStateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(RgbOffState.Address), cancellationToken);
            return RgbOffState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the RgbOffState register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteRgbOffStateAsync(byte[] value, CancellationToken cancellationToken = default)
        {
            var request = RgbOffState.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DI0Mode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DI0ModeConfig> ReadDI0ModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DI0Mode.Address), cancellationToken);
            return DI0Mode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DI0Mode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DI0ModeConfig>> ReadTimestampedDI0ModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DI0Mode.Address), cancellationToken);
            return DI0Mode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DI0Mode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDI0ModeAsync(DI0ModeConfig value, CancellationToken cancellationToken = default)
        {
            var request = DI0Mode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO0Mode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DOModeConfig> ReadDO0ModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0Mode.Address), cancellationToken);
            return DO0Mode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO0Mode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DOModeConfig>> ReadTimestampedDO0ModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO0Mode.Address), cancellationToken);
            return DO0Mode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO0Mode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO0ModeAsync(DOModeConfig value, CancellationToken cancellationToken = default)
        {
            var request = DO0Mode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DO1Mode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DOModeConfig> ReadDO1ModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO1Mode.Address), cancellationToken);
            return DO1Mode.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DO1Mode register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DOModeConfig>> ReadTimestampedDO1ModeAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DO1Mode.Address), cancellationToken);
            return DO1Mode.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DO1Mode register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDO1ModeAsync(DOModeConfig value, CancellationToken cancellationToken = default)
        {
            var request = DO1Mode.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the LatchOnNextUpdate register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<EnableFlag> ReadLatchOnNextUpdateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(LatchOnNextUpdate.Address), cancellationToken);
            return LatchOnNextUpdate.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the LatchOnNextUpdate register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<EnableFlag>> ReadTimestampedLatchOnNextUpdateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(LatchOnNextUpdate.Address), cancellationToken);
            return LatchOnNextUpdate.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the LatchOnNextUpdate register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteLatchOnNextUpdateAsync(EnableFlag value, CancellationToken cancellationToken = default)
        {
            var request = LatchOnNextUpdate.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalInputState register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalInputs> ReadDigitalInputStateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalInputState.Address), cancellationToken);
            return DigitalInputState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalInputState register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalInputs>> ReadTimestampedDigitalInputStateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalInputState.Address), cancellationToken);
            return DigitalInputState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputSet register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputSetAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(OutputSet.Address), cancellationToken);
            return OutputSet.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputSet register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputSetAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(OutputSet.Address), cancellationToken);
            return OutputSet.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputSet register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputSetAsync(DigitalOutputs value, CancellationToken cancellationToken = default)
        {
            var request = OutputSet.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputClear register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputClearAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(OutputClear.Address), cancellationToken);
            return OutputClear.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputClear register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputClearAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(OutputClear.Address), cancellationToken);
            return OutputClear.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputClear register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputClearAsync(DigitalOutputs value, CancellationToken cancellationToken = default)
        {
            var request = OutputClear.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputToggle register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputToggleAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(OutputToggle.Address), cancellationToken);
            return OutputToggle.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputToggle register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputToggleAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(OutputToggle.Address), cancellationToken);
            return OutputToggle.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputToggle register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputToggleAsync(DigitalOutputs value, CancellationToken cancellationToken = default)
        {
            var request = OutputToggle.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the OutputState register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<DigitalOutputs> ReadOutputStateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(OutputState.Address), cancellationToken);
            return OutputState.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the OutputState register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<DigitalOutputs>> ReadTimestampedOutputStateAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(OutputState.Address), cancellationToken);
            return OutputState.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the OutputState register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteOutputStateAsync(DigitalOutputs value, CancellationToken cancellationToken = default)
        {
            var request = OutputState.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalOutputPulsePeriod register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<ushort> ReadDigitalOutputPulsePeriodAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalOutputPulsePeriod.Address), cancellationToken);
            return DigitalOutputPulsePeriod.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalOutputPulsePeriod register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<ushort>> ReadTimestampedDigitalOutputPulsePeriodAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadUInt16(DigitalOutputPulsePeriod.Address), cancellationToken);
            return DigitalOutputPulsePeriod.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DigitalOutputPulsePeriod register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDigitalOutputPulsePeriodAsync(ushort value, CancellationToken cancellationToken = default)
        {
            var request = DigitalOutputPulsePeriod.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the DigitalOutputPulseCount register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<byte> ReadDigitalOutputPulseCountAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalOutputPulseCount.Address), cancellationToken);
            return DigitalOutputPulseCount.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the DigitalOutputPulseCount register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<byte>> ReadTimestampedDigitalOutputPulseCountAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(DigitalOutputPulseCount.Address), cancellationToken);
            return DigitalOutputPulseCount.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the DigitalOutputPulseCount register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteDigitalOutputPulseCountAsync(byte value, CancellationToken cancellationToken = default)
        {
            var request = DigitalOutputPulseCount.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }

        /// <summary>
        /// Asynchronously reads the contents of the EventEnable register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the register payload.
        /// </returns>
        public async Task<RgbArrayEvents> ReadEventEnableAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EventEnable.Address), cancellationToken);
            return EventEnable.GetPayload(reply);
        }

        /// <summary>
        /// Asynchronously reads the timestamped contents of the EventEnable register.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous read operation. The <see cref="Task{TResult}.Result"/>
        /// property contains the timestamped register payload.
        /// </returns>
        public async Task<Timestamped<RgbArrayEvents>> ReadTimestampedEventEnableAsync(CancellationToken cancellationToken = default)
        {
            var reply = await CommandAsync(HarpCommand.ReadByte(EventEnable.Address), cancellationToken);
            return EventEnable.GetTimestampedPayload(reply);
        }

        /// <summary>
        /// Asynchronously writes a value to the EventEnable register.
        /// </summary>
        /// <param name="value">The value to be stored in the register.</param>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        /// <returns>The task object representing the asynchronous write operation.</returns>
        public async Task WriteEventEnableAsync(RgbArrayEvents value, CancellationToken cancellationToken = default)
        {
            var request = EventEnable.FromPayload(MessageType.Write, value);
            await CommandAsync(request, cancellationToken);
        }
    }
}
