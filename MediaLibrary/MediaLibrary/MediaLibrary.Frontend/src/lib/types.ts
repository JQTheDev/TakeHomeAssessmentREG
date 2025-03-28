export type Comment = {
    id: number;
    text: string;
    author: string;
    date: string; // string or date... unsure
    movieId: number;
  }
  
export type Movie = {
    id: number;
    title: string;
    rating: number;
    duration: number;
    year: number;
    popularity: number;
    comments: Comment[];
  }