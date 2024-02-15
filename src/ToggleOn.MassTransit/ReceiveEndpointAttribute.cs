namespace ToggleOn.MassTransit;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class ReceiveEndpointAttribute : Attribute
{
    public string QueueName { get; }
    public string TopicName { get; }

    public ReceiveEndpointAttribute(string queueName, string topicName)
    {
        QueueName = queueName;
        TopicName = topicName;
    }
}
