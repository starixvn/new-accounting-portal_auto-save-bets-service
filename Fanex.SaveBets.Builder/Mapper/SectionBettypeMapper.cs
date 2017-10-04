namespace Fanex.SaveBets.Builder.Mapper
{
    public interface SectionBettypeMapper
    {
        int GetNumberSectionOf(int bettype);

        bool IsMyanmarBettypeGroup(int bettype);
    }
}