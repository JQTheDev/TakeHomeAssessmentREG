<script lang="ts">
  import { fetchMovies } from '$lib/api';
  import type { Movie } from '$lib/types';

  //Using svelte's dynamic nature to navigate between pages
  let currentView: 'home' | 'movies' = 'home';
  let movies: Movie[] = [];
  let isLoading = false;
  let error: boolean = false;

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
</script>

<main>
  {#if currentView == 'home'}
    <div class="home-page">
      <h2>Welcome to Movie Library</h2>
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
      <!-- TODO: continue movies view -->
    </div>
  {/if}
</main>

<style>
  main {
    max-width: 900px;
  }

  .error {
    color: red;
  }

  .home-page {
    text-align: center;
  }

  .movies-view{
    
  }
</style>