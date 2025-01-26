import bme280
import smbus2

def read_bme280():
    port = 1
    address = 0x76
    bus = smbus2.SMBus(port)

    # Wczytaj parametry kalibracyjne
    bme280.load_calibration_params(bus, address)

    # Pobierz próbkę
    bme280_data = bme280.sample(bus, address)
    humidity = bme280_data.humidity
    pressure = bme280_data.pressure
    temperature = bme280_data.temperature

    # Zwróć dane w formacie CSV
    print(f"{humidity},{pressure},{temperature}")

if __name__ == "__main__":
    read_bme280()