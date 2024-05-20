// eslint-disable-next-line @typescript-eslint/no-unused-vars
import styles from './app.module.css';
import { Route, Routes } from 'react-router-dom';
import BooksList from './components/books/books-list/books-list';
import BooksService from './components/books/books.service';
import Menu from './components/menu/menu';
import BookDetails from './components/books/book-details/book-details';

export function App() {

  const booksService = new BooksService();
  
  return (
    <div>
      <Menu title={ 'Book Store' } /> 
      <Routes>
        <Route path='/' element={<BooksList {...{booksService}} />} />
        {/* <Route path='/books/:id' element={<BookDetails booksService={new BooksService()} />} /> */}
        <Route path='/books/:id' element={<BookDetails  {...{booksService}} />} />
      </Routes>
    </div>
  );
}

export default App;
