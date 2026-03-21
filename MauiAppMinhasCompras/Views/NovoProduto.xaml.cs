using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            if (p.Quantidade <= 0)
            {
                await DisplayAlert("Ops", "Quantidade deve ser maior que zero", "OK");
                return;
            }
            else if (p.Preco <= 0)
            {
                await DisplayAlert("Ops", "Preço deve ser maior que zero", "OK");
                return;
            }

                await App.Db.Insert(p);
            await DisplayAlert("Sucesso!", "Produto cadastrado", "OK");

        }
        catch (Exception ex)
        {
           await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}