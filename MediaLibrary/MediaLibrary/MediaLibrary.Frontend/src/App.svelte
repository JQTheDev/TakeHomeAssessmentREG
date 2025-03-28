<script lang="ts">
  import { fetchMovies, getCommentsForMovie, errorMsg } from '$lib/api';
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
      alert(`Error: ${errorMsg}`)
    } finally {
      isLoading = false;
    }
  }

  function goHome() {
    currentView = 'home';
  }

  async function displayComments(movieId: number) {
    if (selectedMovieId == movieId) {
      selectedMovieId = null;
      return; //If same button is clicked, close comments & full details
    }
    selectedMovieId = movieId;
    try {
      comments = await getCommentsForMovie(movieId);
    }
    catch {
      comments = []
      alert(`Error: ${errorMsg}`)
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
  const hoursAgo = Math.floor((currentTime - commentTime) / (1000 * 60 * 60)); //Compares current time to comment time to see how many hours ago comment was posted
  
  return `${hoursAgo} hours ago`;
}

function ratingColour(rating: number) {
  if (rating <= 3){
    return "low-rating"
  }
  else if (rating > 3 && rating <= 6){
    return "mid-rating"
  }
  else{
    return "high-rating"
  }
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
    <h1>Movie List</h1>
    <button on:click={goHome}>Go Back</button>
    {#each movies as movie}
      <div class="movie-items">
        {movie.title} ({movie.year})
          <b>Rating:</b>
          <span class={`${ratingColour(movie.rating)}`}>{movie.rating}/10</span>
        <button 
        on:click={() => displayComments(movie.id)}
        class:active={selectedMovieId == movie.id}> 
        {selectedMovieId == movie.id ? 'Hide Info' : 'More Info'}</button>
      </div>

      {#if selectedMovieId == movie.id}
      <div class="more-info">
        <p><b>Duration:</b>{HrsAndMins(movie.duration)}</p>
        <p><b>{movie.popularity}</b> Have watched within the last 24 hours</p>
          <p style="font-size: 1rem;">Comments:</p>
          {#if comments.length > 0}
            {#each comments as comment}
            <div class="comments">
              <p>{comment.author} -</p>
              <p id="each-comment">"{comment.text}"</p>
              <p>({getHoursAgo(comment.date)})</p>
            </div>
            {/each}
          {/if}
      </div>
      {/if}
    {/each}

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
  color: black;
}

.high-rating{
  color: green;
}

.mid-rating{
  color: yellow;
}

.comments{
  display: flex;
  justify-content: center;
}

.low-rating{
  color: red;
}

.more-info p {
  margin: 0.4rem 0;
}

  .comments-section{
    display: block;
  }
  .movie-items{
    margin: 0.5rem;
  }

  .error {
    color: red;
  }

  #each-comment{
    display:block;
    margin-bottom: 0.5rem;
    margin-right: 0.5rem;
    margin-left: 0.5rem;
    font-weight: 600;
  }
  .home-page {
    text-align: center;
  }

  
</style>