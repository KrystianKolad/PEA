namespace PEA.Consts
{
    public static class GlobalConsts
    {
        public const string AlgorithmChoice = @"Wybierz alborytm:
        1.TSP Dynamiczny
        2.ATSP Dynamiczny
        3.TSP Tabu search
        4.ATSP Tabu search
        5.TSP Genetyczny
        6.ATSP Genetyczny";
        public const string TSPFileChoice = @"Wybierz plik:
        1.berlin55
        2.ch130
        3.d18512";
        public const string ATSPFileChoice = @"Wybierz plik:
        1.ftv55
        2.ft70
        3.rbg403";
        public const string AfterMessage = "@Czy chcesz uruchomic kolejny algorytm? y/n";
        public const string TSPFilePath = @"Data/TSP/";
        public const string ATSPFilePath = @"Data/ATSP/";
        public const int TSPLinesToCutOff = 6;
        public const int ATSPLinesToCutOff = 7;
    }
}