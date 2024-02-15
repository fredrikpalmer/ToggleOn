namespace ToggleOn.MassTransit;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class SubscriptionEndpointAttribute : Attribute
{
    public string SubscriptionName { get; }
    public string TopicName { get; }

    public SubscriptionEndpointAttribute(string subscriptionName, string topicName)
    {
        SubscriptionName = subscriptionName;
        TopicName = topicName;
    }
}
