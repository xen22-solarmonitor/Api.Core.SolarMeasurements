namespace Api.Core.SolarMeasurements.Models
{
    // TODO: find a more appropriate name for this type

    /// <summary>
    /// The function that is applied to a range of sensor measurements to obtain a single value that
    /// represents that range.
    /// e.g. when requesting the average value for a sensor over a period of a day, the Average reducer
    /// function can be specified, along with a Granularity value of "Daily".!--
    /// Note: not all types of reducers may be applicable to all sensors.
    /// </summary>
    public enum ReducerFunction
    {
        Average,
        Minimum,
        Maximum
    }
}