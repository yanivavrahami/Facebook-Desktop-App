﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace UI
{
    public class FormFactory
    {
        public Color BackgroundColor { get; set; }

        public FormFactory(Color i_BackgroundColor)
        {
            BackgroundColor = i_BackgroundColor;
        }

        public Form Create(Type i_FormType)
        {
            Form newForm = null;

            if (i_FormType.Equals(typeof(AlbumsForm)))
            {
                newForm = new AlbumsForm(this);
            }
            else if (i_FormType.Equals(typeof(AlbumForm)))
            {
                newForm = new AlbumForm();
            }
            else if (i_FormType.Equals(typeof(FriendsForm)))
            {
                newForm = new FriendsForm();
            }
            else if (i_FormType.Equals(typeof(PostsForm)))
            {
                newForm = new PostsForm();
            }
            else if (i_FormType.Equals(typeof(EventsForm)))
            {
                newForm = new EventsForm();
            }
            else if (i_FormType.Equals(typeof(GroupsForm)))
            {
                newForm = new GroupsForm();
            }
            else if (i_FormType.Equals(typeof(LikeRatedForm)))
            {
                newForm = new LikeRatedForm();
            }
            else if (i_FormType.Equals(typeof(PostsCounterForm)))
            {
                newForm = new PostsCounterForm();
            }
            
            if (newForm != null)
            {
                newForm.BackColor = BackgroundColor;
                return newForm;
            }

            throw new ArgumentException("Not found " + i_FormType + "type.");
        }
    }
}
