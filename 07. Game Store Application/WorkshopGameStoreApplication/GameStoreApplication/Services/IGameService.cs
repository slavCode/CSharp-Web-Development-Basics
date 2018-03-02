namespace GameStoreApplication.Services
{
    using System.Collections.Generic;
    using ViewModels.Admin;

    public interface IGameService
    {
        void Create(GameViewModel model);

        IEnumerable<ListGameViewModel> All();

        GameViewModel FindById(int id);

        void Edit(GameViewModel model);

        void DeleteById(int id);

        List<string> AnonymousGamePaths();
    }
}
