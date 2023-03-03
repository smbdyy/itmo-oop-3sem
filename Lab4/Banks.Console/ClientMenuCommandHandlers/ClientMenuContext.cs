using Banks.Clients;
using Banks.Console.Tools.Exception;

namespace Banks.Console.ClientMenuCommandHandlers;

public class ClientMenuContext
{
    private BankClient? _client;

    public BankClient Client
    {
        get
        {
            if (_client is null)
            {
                throw new ContextNotSetException();
            }

            return _client;
        }
    }

    public ClientMenuContext SetClient(BankClient client)
    {
        _client = client;
        return this;
    }
}