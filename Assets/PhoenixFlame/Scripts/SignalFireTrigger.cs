public class SignalFireTrigger:ISignal
{
    public readonly bool isOn;

    public SignalFireTrigger(bool isOn)
    {
        this.isOn = isOn;
    }
}