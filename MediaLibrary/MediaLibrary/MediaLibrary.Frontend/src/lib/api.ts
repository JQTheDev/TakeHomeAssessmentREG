import type { Movie, Comment } from './types';

export let errorMsg: string = ""

export async function fetchMovies(): Promise<Movie[]> {
  try {
    const response = await fetch("http://localhost:5170/api/movies");

    if (!response.ok) {
      errorMsg = `An error occurred while trying to get all movies. Status Code: ${response.status}`;
      throw new Error(errorMsg);
    }
    
    const movies: Movie[] = await response.json()
    return movies

  } catch (error) {
    console.error("Error fetching movies:", error);
    throw error; //allows component to handle error
  }
}

export async function getCommentsForMovie(id: number): Promise<Comment[]> {
    try {
        const response = await fetch(`http://localhost:5170/api/movies/${id}/comments`);
        if (!response.ok){
          if (response.status == 404) {
            errorMsg = (`there are no comments for the movie with the ID: ${id} Status Code: ${response.status}`);
            throw new Error(errorMsg)
          }
            errorMsg = (`An error occured while getting the comments for the movie with the corresponding Id: ${id}. Status Code: ${response.status}`); 
            throw new Error(errorMsg)
        }
        const comments: Comment[] = await response.json();
        return comments

      } catch (error) {
        console.error("Failed to load comments:", error);
        throw error;
      }
}
