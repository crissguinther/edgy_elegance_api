namespace EdgyElegance.Application.Constants;

public class ApplicationConstants {
    public static string CONNECTION_STRING { get => Environment.GetEnvironmentVariable("EdgyEleganceConnectionString") ?? string.Empty; } 
    public static string IMAGE_UPLOAD_FOLDER { get => Environment.GetEnvironmentVariable("EdgyEleganceImageUploadFolder") ?? string.Empty; }
}
