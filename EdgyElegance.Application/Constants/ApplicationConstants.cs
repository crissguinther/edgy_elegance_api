namespace EdgyElegance.Application.Constants;

public class ApplicationConstants {
    public static string CONNECTION_STRING { get => Environment.GetEnvironmentVariable("EdgyEleganceConnectionString") ?? string.Empty; } 
}
