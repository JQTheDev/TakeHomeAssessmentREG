document.addEventListener("DOMContentLoaded", async () => {
    try {
        const response = await fetch("http://localhost:5170/api/movies"); //fullpath not needed but placeholder incase A framework is added depending on time
        if (!response.ok) {
            alert("Failed to fetch movies");
            throw new Error("Failed to fetch movies");
        } 

        const movies = await response.json();
        const movieList = document.getElementById("movieList");

        movies.forEach(movie => {
            const li = document.createElement("li");
            li.textContent = `${movie.title} (${movie.year})`;
            movieList.appendChild(li);
        });
    } catch (error) {
        console.error("Error loading movies:", error.message);
    }
});


//TODO: add spinner if movies take a while to load. Requires framework?