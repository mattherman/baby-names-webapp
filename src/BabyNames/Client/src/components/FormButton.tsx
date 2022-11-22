import React from 'react';
import styles from './FormButton.css';

interface IFormButtonProps {
	children: string;
	onClick?: React.MouseEventHandler<HTMLButtonElement>;
}

function FormButton({ children, onClick }: IFormButtonProps) {
	return (
		<button type="submit" className={styles.root} onClick={onClick}>
			{children}
		</button>
	);
}

export default FormButton;
