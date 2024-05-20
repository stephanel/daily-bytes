import React from 'react';
import BooksService from '../books.service';
import styles from './books-list.module.css';
import { Book } from '../book.model';

/* eslint-disable-next-line */
export interface BooksListProps {
  readonly booksService: BooksService
}

interface BooksState {
  readonly books: Book[]
}

export class BooksList extends React.Component<BooksListProps, BooksState> {

  constructor(props: BooksListProps) {
    super(props);
    this.state = { books: [] };
  }

  componentDidMount() {
    this.props.booksService.getAll().subscribe((books: Book[]) => {
      this.setState({ books });
    });  
  }

  render() {
    const { books } = this.state

    
    const booksList = books.map((book) => (
      <li key={book.id}>
        <div className="card mb-3 border-white" style={{maxWidth: '680px'}}>
            <div className="row g-0">
                <div className="col-md-4">
                    <img className="img-fluid rounded-start" src={book?.thumbnailUrl} alt="{book?.title}" />
                </div>
                <div className="col-md-8">
                    <div className="card-body">
                        <h5 className="card-title">
                            {book?.title}
                        </h5>
                        <div className="card-text">
                            ISBN: {book?.isbn}
                        </div>
                        <div className="card-text">
                            Language: {book?.language}
                        </div>
                        <div className="card-text">
                            Authors: {book.authors.map((author) => `${author?.firstName} ${author?.lastName}`).join(', ')}
                        </div>
                        <a href={'/book/' + book?.id}>View</a>
                    </div>
                </div>
            </div>
        </div>
      </li>
    ));

    return (
      <div>
        <ul className={styles.books}>{booksList}</ul>
      </div>
    );
  }
}


// export class BooksList extends React.Component<BooksListProps, BooksState> {

//   constructor(props: BooksListProps) {
//     super(props)

//     props.booksService.getAll().subscribe((books: Book[]) => {
//       // const xstate: BooksState = { books };
//       // this.state = xstate;
//       this.state = { books };
//     });
//   }

//   render() {
//     const booksList = this.state.books.map((book) => (
//       <li key={book.id}>
//         {book.title}
//       </li>
//     ));

//     return (
//       <div className={styles['container']}>
//         <ul>{booksList}</ul>
//         <pre>{JSON.stringify(this.state.books, null, 2)}</pre>
//       </div>
//     );
//   }
// }

export default BooksList;
