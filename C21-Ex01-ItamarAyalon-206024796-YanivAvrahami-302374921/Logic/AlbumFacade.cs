﻿using FacebookWrapper.ObjectModel;
using System.Collections.ObjectModel;

namespace Logic
{
    public class AlbumsFacade
    {
        private Photos m_Photos;

        public Photos CurrentChunk { get; }
        public int ChunkSize { get; private set; }
        public int MaxChunkSize { get; }

        public AlbumsFacade(Collection<Photo> i_Photos, int i_MaxChunkSize)
        {
            m_Photos = new Photos(i_Photos);
            MaxChunkSize = i_MaxChunkSize;
            CurrentChunk = null;
        }

        public void PrevPage()
        {
            CurrentChunk.Clear();

            for (int i = 0; i < MaxChunkSize; i++)
            {
                CurrentChunk.Add(m_Photos.Current);

                if (!m_Photos.MovePrev())
                {
                    break;
                }
            }
        }

        public void NextPage()
        {
            CurrentChunk.Clear();

            for (int i = 0; i < MaxChunkSize; i++)
            {
                if (!m_Photos.MoveNext())
                {
                    break;
                }

                CurrentChunk.Add(m_Photos.Current);
            }
        }

        public bool HasNextPage()
        {
            return (m_Photos.Index < m_Photos.Count);
        }

        public bool HasPrevPage()
        {
            return (m_Photos.Index >= MaxChunkSize);
        }
    }
}
