import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { solid } from '@fortawesome/fontawesome-svg-core/import.macro';
import React from 'react';
import { IBabyName, NameGender } from '~/models';
import styles from './ResultRow.css';

interface IResultRowProps {
	babyName: IBabyName;
}

function ResultRow({ babyName }: IResultRowProps) {
	return (
		<div className={styles.root}>
			<span className={styles.icon}>
				{babyName.gender == NameGender.Male ? (
					<FontAwesomeIcon icon={solid('mars')} />
				) : (
					<FontAwesomeIcon icon={solid('venus')} />
				)}
			</span>
			<span className={styles.name}>{babyName.name}</span>
		</div>
	);
}

export default ResultRow;
