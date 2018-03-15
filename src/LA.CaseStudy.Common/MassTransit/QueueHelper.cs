namespace LA.CaseStudy.Common.MassTransit
{
    public static class QueueHelper
    {
        public static string GetQueueName<T>() => typeof(T).Name;
    }
}