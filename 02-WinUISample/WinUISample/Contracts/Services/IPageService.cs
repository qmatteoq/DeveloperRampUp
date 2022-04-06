using System;

namespace WinUISample.Contracts.Services
{
    public interface IPageService
    {
        Type GetPageType(string key);
    }
}
