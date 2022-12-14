import React, { ReactElement } from 'react';
import styles from './Button.css';

interface IButtonProps {
	children: string | ReactElement[];
	onClick?: React.MouseEventHandler<HTMLButtonElement>;
}

function Button({ children, onClick }: IButtonProps) {
	return (
		<button className={styles.root} onClick={onClick}>
			{children}
		</button>
	);
}

export default Button;
