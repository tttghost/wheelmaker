public interface IPanel : IUI
{
    panel_Base SetData<T2>(T2 t2);
    panel_Base SetData<T1, T2>(T1 t1, T2 t2);
}