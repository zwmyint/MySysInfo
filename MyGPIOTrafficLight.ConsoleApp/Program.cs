﻿using System.Device.Gpio;

// https://github.com/simonprickett/dotnetpitrafficlights/tree/main

const int redPin = 9;
const int amberPin = 10;
const int greenPin = 11;

GpioController gpio = new GpioController();
gpio.OpenPin(redPin, PinMode.Output);
gpio.OpenPin(amberPin, PinMode.Output);
gpio.OpenPin(greenPin, PinMode.Output);

void AllLightsOff()
{
    gpio.Write(redPin, PinValue.Low);
    gpio.Write(amberPin, PinValue.Low);
    gpio.Write(greenPin, PinValue.Low);
}

Console.CancelKeyPress += delegate {
    AllLightsOff();
};

AllLightsOff();

while (true)
{
    // Red
    gpio.Write(redPin, PinValue.High);
    Thread.Sleep(3000);

    // Red and amber
    gpio.Write(amberPin, PinValue.High);
    Thread.Sleep(1000);

    // Green
    gpio.Write(redPin, PinValue.Low);
    gpio.Write(amberPin, PinValue.Low);
    gpio.Write(greenPin, PinValue.High);
    Thread.Sleep(5000);

    // Amber
    gpio.Write(greenPin, PinValue.Low);
    gpio.Write(amberPin, PinValue.High);
    Thread.Sleep(2000);

    // Amber off
    gpio.Write(amberPin, PinValue.Low);
}
