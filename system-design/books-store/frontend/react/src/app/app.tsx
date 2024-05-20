// eslint-disable-next-line @typescript-eslint/no-unused-vars
import styles from './app.module.css';
import BooksList from './components/books/books-list/books-list';
import BooksService from './components/books/books.service';
import Menu from './components/menu/menu';

// import NxWelcome from './nx-welcome';

export function App() {
  return (
    <div>
      <Menu title={ 'Book Store' } /> 
      {/* <NxWelcome title="book-store" /> */}
      <BooksList booksService={ new BooksService } />
    </div>
  );
}

export default App;
