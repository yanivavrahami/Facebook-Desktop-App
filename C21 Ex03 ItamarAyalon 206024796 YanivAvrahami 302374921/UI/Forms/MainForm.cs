﻿using System;
using System.Drawing;
using System.Windows.Forms;
using FacebookWrapper;
using Logic;

namespace UI
{
    public partial class MainForm : Form
    {
        private readonly FacebookUserFetcher r_FacebookUserFetcher = FacebookUserFetcher.Instance;

        private ICommand m_EventsFormCommand;
        private ICommand m_FriendsCommand;
        private ICommand m_PostsCommand;
        private ICommand m_AlbumsCommand;
        private ICommand m_GroupsCommand;
        private ICommand m_LikeRatedCommand;
        private ICommand m_PostsCounterCommand;

        public MainForm()
        {
            InitializeComponent();
            initCommands();

            setButtons(false);
            FacebookService.s_CollectionLimit = 100;

            checkBoxRememberMe.Checked = Logic.Properties.Settings.Default.RememberMe;
        }

        private void initCommands()
        {
            FormFactory formFactory = new FormFactory(Color.FromArgb(240, 242, 245));
            m_EventsFormCommand = new RelayCommand(() => formFactory.Create(typeof(EventsForm)).ShowDialog());
            m_FriendsCommand = new RelayCommand(() => formFactory.Create(typeof(FriendsForm)).ShowDialog());
            m_PostsCommand = new RelayCommand(() => formFactory.Create(typeof(PostsForm)).ShowDialog());
            m_AlbumsCommand = new RelayCommand(() => formFactory.Create(typeof(AlbumsForm)).ShowDialog());
            m_GroupsCommand = new RelayCommand(() => formFactory.Create(typeof(GroupsForm)).ShowDialog());
            m_LikeRatedCommand = new RelayCommand(() => formFactory.Create(typeof(LikeRatedForm)).ShowDialog());
            m_PostsCounterCommand = new RelayCommand(() => formFactory.Create(typeof(PostsCounterForm)).ShowDialog());
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SetSelectionBarOnButton((Button)sender);
            Clipboard.SetText("design.patterns.c21ב");

            LoginResult loginResult = r_FacebookUserFetcher.Login();

            if (!string.IsNullOrEmpty(loginResult.AccessToken))
            {
                setButtons(true);
                toolbarFetch();
                profileFetch();
            }
            else
            {
                MessageBox.Show(loginResult.ErrorMessage, "Login Failed");
            }
        }

        private void profileFetch()
        {
            pictureBoxProfilePicture.Image = r_FacebookUserFetcher.User.ImageLarge;
            labelProfileFirstName.Text = r_FacebookUserFetcher.User.FirstName;
            labelProfileLastName.Text = r_FacebookUserFetcher.User.LastName;
            labelProfileGender.Text = r_FacebookUserFetcher.User.Gender.ToString();
            labelProfileLocation.Text = r_FacebookUserFetcher.User.Location?.Name;
            labelProfileEmail.Text = r_FacebookUserFetcher.User.Email;
            labelProfileBirthday.Text = r_FacebookUserFetcher.User.Birthday;
            labelProfileStatus.Text = r_FacebookUserFetcher.User.RelationshipStatus?.ToString();
        }

        private void clear()
        {
            pictureBoxProfilePicture.Image = null;

            labelProfileFirstName.Text = string.Empty;
            labelProfileLastName.Text = string.Empty;
            labelProfileGender.Text = string.Empty;
            labelProfileLocation.Text = string.Empty;
            labelProfileEmail.Text = string.Empty;
            labelProfileBirthday.Text = string.Empty;
        }

        private void setButtons(bool i_IsActive)
        {
            btnEvents.Enabled = i_IsActive;
            btnFriends.Enabled = i_IsActive;
            btnPosts.Enabled = i_IsActive;
            btnAlbums.Enabled = i_IsActive;
            btnGroups.Enabled = i_IsActive;
            btnLogout.Enabled = i_IsActive;
            btnLikeRated.Enabled = i_IsActive;
            btnPostsCounter.Enabled = i_IsActive;
            btnLogin.Enabled = !i_IsActive;
        }

        private void toolbarFetch()
        {
            pictureBoxProfile.LoadAsync(r_FacebookUserFetcher.User.PictureSmallURL);
            labelUserName.Text = r_FacebookUserFetcher.User.Name;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            SetSelectionBarOnButton((Button)sender);
            
            FacebookService.LogoutWithUI();
            r_FacebookUserFetcher.Logout();
            pictureBoxProfile.Image = UI.Properties.Resources.icons8_name_25;
            labelUserName.Text = string.Empty;
            clear();
            setButtons(false);
        }

        private void displayDialog(Button i_Button, ICommand i_Command)
        {
            SetSelectionBarOnButton(i_Button);
            i_Command.Execute();
        }

        private void btnEvents_Click(object sender, EventArgs e)
        {
            displayDialog((Button)sender, m_EventsFormCommand);
        }

        private void btnFriends_Click(object sender, EventArgs e)
        {
            displayDialog((Button)sender, m_FriendsCommand);
        }

        private void btnPosts_Click(object sender, EventArgs e)
        {
            displayDialog((Button)sender, m_PostsCommand);
        }

        private void btnAlbums_Click(object sender, EventArgs e)
        {
            displayDialog((Button)sender, m_AlbumsCommand);
        }

        private void btnGroups_Click(object sender, EventArgs e)
        {
            displayDialog((Button)sender, m_GroupsCommand);
        }

        private void btnLikeRated_Click(object sender, EventArgs e)
        {
            displayDialog((Button)sender, m_LikeRatedCommand);
        }

        private void btnPostsCounter_Click(object sender, EventArgs e)
        {
            displayDialog((Button)sender, m_PostsCounterCommand);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SetSelectionBarOnButton(Button i_Button) 
        {
            selectionBar.Width = i_Button.Width;
            selectionBar.Location = new Point(i_Button.Location.X, selectionBar.Location.Y);
            selectionBar.BringToFront();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private bool mouseDown;
        private Point lastLocation;

        private void panelMenu_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void panelMenu_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void panelMenu_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void checkBoxRememberMe_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            Logic.Properties.Settings.Default.RememberMe = checkBox.Checked;
            Logic.Properties.Settings.Default.Save();
        }
    }
}