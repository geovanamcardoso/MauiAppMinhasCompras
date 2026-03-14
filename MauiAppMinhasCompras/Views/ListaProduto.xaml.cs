using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();	
    public ListaProduto()
	{
		InitializeComponent();
		lst_produtos.ItemsSource = lista;
	}

	protected override async void OnAppearing()
	{
		List<Produto> tmp = await App.Db.GetAll(); // Busca os dados do banco de dados
        tmp.ForEach(i => lista.Add(i)); // Adiciona os dados na lista que é a fonte de dados do ListView
    }
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try 
		{
			Navigation.PushAsync(new Views.NovoProduto());
        }
		catch (Exception ex) 
		{
			DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
		string q = e.NewTextValue;

		lista.Clear(); // Limpa a lista para mostrar os resultados da busca

        List<Produto> tmp = await App.Db.Search(q); 
        tmp.ForEach(i => lista.Add(i));

    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"O valor total dos produtos é: {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem item = sender as MenuItem;
            Produto produtoSelecionado = item.BindingContext as Produto;

            await App.Db.Delete(produtoSelecionado.Id);
            lista.Remove(produtoSelecionado);
            
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}