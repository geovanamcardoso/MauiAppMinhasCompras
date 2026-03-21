namespace MauiAppMinhasCompras.Views;
using MauiAppMinhasCompras.Models;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto produto_anexado = BindingContext as Produto;

            Produto p = new Produto
            {
                Id = produto_anexado.Id,
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

            await App.Db.Update(p);
            await DisplayAlert("Sucesso!", "Produto atualizado", "OK");

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}