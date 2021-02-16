using BL;

namespace BLAPI
{
    public static class BLFactory
    {
        public static IBL GetBL() => new BLImp();
    }
}
