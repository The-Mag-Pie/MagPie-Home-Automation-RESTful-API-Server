namespace MagPie_Home_Automation_RESTful_API_Server.BackgroundTasks
{
    public class BackgroundTasksStarter
    {
        public static void Start()
        {
            new RoomClimateReader();
        }
    }
}
