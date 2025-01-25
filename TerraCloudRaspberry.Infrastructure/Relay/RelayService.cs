using System.Device.Gpio;

namespace TerraCloudRaspberry.Infrastructure.Relay
{
    internal class RelayService : IRelayService
    {
        private readonly GpioController _gpioController;
        private const int pin = 20;

        public RelayService()
        {
            _gpioController = new GpioController();
            _gpioController.OpenPin(pin, PinMode.Output);
        }
        public void TurnOff()
        {
            _gpioController.Write(pin, PinValue.Low);
        }

        public void TurnOn()
        {
            _gpioController.Write(pin, PinValue.High);
        }
    }
}
