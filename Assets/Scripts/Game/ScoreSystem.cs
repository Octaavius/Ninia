public static class ScoreSystem
{
    public static int getScoreFromObstacle(string obstacleTag) {
        switch (obstacleTag) {
            case "Pillow": 
                return 7;
            default: 
                return 0;
        }
    } 
}
