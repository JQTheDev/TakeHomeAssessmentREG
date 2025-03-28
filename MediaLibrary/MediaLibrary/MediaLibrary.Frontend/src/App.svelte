<script lang="ts">
  import { fetchMovies, getCommentsForMovie } from '$lib/api';
  import type { Movie, Comment } from '$lib/types';

  //Using svelte's dynamic nature to navigate between pages
  let currentView: 'home' | 'movies' = 'home';
  let movies: Movie[] = [];
  let isLoading = false;
  let error: boolean = false;
  let selectedMovieId: number | null = null
  let comments: Comment[] = []

  async function loadMovies(): Promise<void> {
    isLoading = true;
    error = false;
    try {
      movies = await fetchMovies();
      currentView = 'movies';
    } catch (err) {
      error = true;
    } finally {
      isLoading = false;
    }
  }

  function goHome() {
    currentView = 'home';
  }

  async function displayComments(movieId: number) {
    if (selectedMovieId === movieId) {
      selectedMovieId = null;
      return; //If same button is clicked, close comments & full details
    }
    selectedMovieId = movieId;
    try {
      comments = await getCommentsForMovie(movieId);
    }
    catch {
      //some error handling
    }
    

  }

  function HrsAndMins(num: number): string {

    let hours = Math.floor(num/60)
    let minutes = num % 60

    return `  ${hours}hrs ${minutes}mins`
  }

  function getHoursAgo(commentDate: string): string {
  const commentTime = new Date(commentDate).getTime();
  const currentTime = Date.now();
  const hoursAgo = Math.floor((currentTime - commentTime) / (1000 * 60 * 60)); //Compares current time to comment time to see how many hours ago the comment was posted
  
  return `${hoursAgo} hours ago`;
  //considered sorting numbers so that most recent was displayed first but it would increase overall time complex.
}


</script>

<main>
  {#if currentView == 'home'}
    <div class="home-page">
      <h2>Welcome to the Movie Library</h2>
      <h4>Here you can see information about the most popular movies out right now. 
          Click below to see what we have available!</h4>
      <button on:click={loadMovies} disabled={isLoading}>
        <p>{isLoading ? 'Loading...' : 'Browse Movies'}</p>
      </button>
      {#if error}
        <p class="error">An error occured when triyng to get the movies. Please try again later</p>
      {/if}
    </div>

  {:else}
    
    <div class="movies-view">
      <h1>Movie List</h1>
      <button on:click={goHome}>Go Back</button>
      {#each movies as movie}
        <div class="movie-items">
          {movie.title} ({movie.year})
          <span class="rating"> {movie.rating}/10</span>
          <button 
          on:click={() => displayComments(movie.id)}
          class:active={selectedMovieId === movie.id}> 
          {selectedMovieId === movie.id ? 'Hide Info' : 'More Info'}</button>
        </div>

        {#if selectedMovieId === movie.id}
        <div class="more-info">
          <p><b>Duration:</b>{HrsAndMins(movie.duration)}</p>
          <p><b>{movie.popularity}</b> Have watched within the last 24 hours</p>
            <p style="font-size: 1rem;">Comments:</p>
            {#if comments.length > 0}
              {#each comments as comment}
              <div style="display:flex;">
                <p id="each-comment">{comment.text}</p>
                <p>({getHoursAgo(comment.date)})</p>
              </div>
              {/each}
            {/if}
        </div>
        {/if}
      {/each}

    </div>
  {/if}
</main>

<style>
  main {
    max-width: 900px;
  }

  .more-info {
  margin-top: 1rem;
  padding: 0.8rem;
  background: grey;
  border-radius: 6px;
}

.more-info p {
  margin: 0.4rem 0;
}

  .comments-section{
    display: block;
  }
  .move-items{
    border: 5px;
  }

  .error {
    color: red;
  }

  #each-comment{
    display:block;
    margin-bottom: 0.5rem;
    margin-right: 0.5rem;
  }
  .home-page {
    text-align: center;
  }

  .movies-view{
    
  }

  
</style>