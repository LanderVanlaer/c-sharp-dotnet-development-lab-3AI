using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Windows;
using Newtonsoft.Json;

namespace users_list;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        AddUserButton.Click += SubmitUser;
        DeleteUserButton.Click += DeleteUserAction;

        SortListByFirstNameButton.Click += SortListByFirstNameAction;
        SortListByLastNameButton.Click += SortListByLastNameAction;

        UploadListButton.Click += UploadListAction;
        CountToMaxIntButton.Click += CountToMaxIntAction;

        DataContext = this;
    }

    public ObservableCollection<User> Users { get; } = new()
    {
        new User("John", "Doe"),
        new User("Eve", "Born"),
        new User("Mickael", "Tink"),
        new User("Emma", "Whoosh"),
    };

    private void DeleteUserAction(object sender, RoutedEventArgs e)
    {
        int i = UsersListView.SelectedIndex;

        if (i < 0 || Users.Count <= i) return;

        Users.RemoveAt(i);
    }

    /// <summary>
    ///     https://www.youtube.com/watch?v=IVIzujZi07Q&amp;ab_channel=TacticDevs
    /// </summary>
    private void SortListByFirstNameAction(object sender, RoutedEventArgs e)
    {
        UsersListView.Items.SortDescriptions.Clear();
        UsersListView.Items.SortDescriptions.Add(new SortDescription(nameof(User.FirstName),
            ListSortDirection.Ascending));
    }

    private void SortListByLastNameAction(object sender, RoutedEventArgs e)
    {
        UsersListView.Items.SortDescriptions.Clear();
        UsersListView.Items.SortDescriptions.Add(new SortDescription(nameof(User.LastName),
            ListSortDirection.Ascending));
    }

    private async void UploadListAction(object sender, RoutedEventArgs e)
    {
        UploadListButton.IsEnabled = false;

        string content =
            await new HttpClient()
                .GetStringAsync("http://www.seamine.be/xamarin.php?list=" + JsonConvert.SerializeObject(Users));

        UploadListButton.IsEnabled = true;

        if (content == "1")
            MessageBox.Show("Uploaded JSON successfully", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
        else
            MessageBox.Show("Uploading JSON failed!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    private async void CountToMaxIntAction(object sender, RoutedEventArgs e)
    {
        CountToMaxIntButton.IsEnabled = false;

        await Task.Run(() =>
        {
            for (long i = 0; i < 10737418235L; i++) // (long)Int32.MaxValue * 5
                if (i % 100000000 == 0)
                    Console.WriteLine(i);
        });

        await Console.Out.FlushAsync();
        CountToMaxIntButton.IsEnabled = true;
    }

    private void SubmitUser(object sender, RoutedEventArgs e)
    {
        string firstName = FirstName.Text.Trim();
        string lastName = LastName.Text.Trim();

        if (string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(firstName))
        {
            MessageBox.Show("First name and/or last name could not be empty", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return;
        }

        User user = new(firstName, lastName);
        Users.Add(user);

        FirstName.Clear();
        LastName.Clear();

        MessageBox.Show(user + " added to list", "Added user", MessageBoxButton.OK,
            MessageBoxImage.Information);
    }
}