import styles from './book-details.module.css';

/* eslint-disable-next-line */
export interface BookDetailsProps {}

export function BookDetails(props: BookDetailsProps) {
  return (
    <div className={styles['container']}>
      <h1>Welcome to BookDetails!</h1>
    </div>
  );
}

export default BookDetails;
