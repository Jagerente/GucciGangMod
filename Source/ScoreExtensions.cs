using ExitGames.Client.Photon;

internal static class ScoreExtensions
{
    public static void AddScore(this PhotonPlayer player, int scoreToAddToCurrent)
    {
        var num = player.GetScore() + scoreToAddToCurrent;
        var propertiesToSet = new Hashtable();
        propertiesToSet["score"] = num;
        player.SetCustomProperties(propertiesToSet);
    }

    public static int GetScore(this PhotonPlayer player)
    {
        object obj2;
        if (player.customProperties.TryGetValue("score", out obj2))
        {
            return (int) obj2;
        }

        return 0;
    }

    public static void SetScore(this PhotonPlayer player, int newScore)
    {
        var propertiesToSet = new Hashtable();
        propertiesToSet["score"] = newScore;
        player.SetCustomProperties(propertiesToSet);
    }
}