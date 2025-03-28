import type { Movie, Comment } from './types';

export async function fetchMovies(): Promise<Movie[]> {
  try {
    const response = await fetch("http://localhost:5170/api/movies");

    if (!response.ok) {
      throw new Error(`An error occured while trying to get all movies. Status Code:${response.status}.`);
    }
    const movies: Movie[] = await response.json()
    return movies

  } catch (error) {
    console.error("Error fetching movies:", error);
    throw error; //allows components to handle error
  }
}

export async function getCommentsForMovie(id: number): Promise<Comment[]> {
    try {
        const response = await fetch(`http://localhost:5170/api/movies/${id}/comments`);
        if (!response.ok){
            throw new Error(`An error occured while getting the comments for the movie with the corresponding id:${id}. Status Code:${response.status}.`); 
        }
        const comments: Comment[] = await response.json();
        return comments

      } catch (error) {
        console.error("Failed to load comments:", error);
        throw error;
      }
}