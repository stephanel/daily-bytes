import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Book } from '../book.model';
import BooksService from '../books.service';
import styles from './book-details.module.css';

/* eslint-disable-next-line */
export interface BookDetailsProps {
  readonly booksService: BooksService;
}

export default function BookDetails(props: {booksService: BooksService}) {
  const [book, setBook] = useState<Book | null>(null);
  const { id } = useParams();

  useEffect(() => {
    props.booksService.get(Number(id)).subscribe((book: Book) => {
      setBook(book);
    });
  }, [id]);

  return (
    <div className={styles['container']}>
      <div className="card mb-3 border-white" style={{ maxWidth: '680px' }}>
        <div className="row g-0">
          <div className="col-md-4">
            <img
              className="img-fluid rounded-start"
              src={book?.thumbnailUrl}
              alt="{book?.title}"
            />
          </div>
          <div className="col-md-8">
            <div className="card-body">
              <h5 className="card-title">{book?.title}</h5>
              <div className="card-text">ISBN: {book?.isbn}</div>
              <div className="card-text">Language: {book?.language}</div>
              <div className="card-text">
                Authors:{' '}
                {book?.authors
                  .map((author) => `${author?.firstName} ${author?.lastName}`)
                  .join(', ')}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

// export default BookDetails;