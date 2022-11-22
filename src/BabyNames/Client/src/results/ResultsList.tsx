import { solid } from '@fortawesome/fontawesome-svg-core/import.macro';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useState } from 'react';
import { IBabyName } from '~/models';
import ResultRow from './ResultRow';
import styles from './ResultsList.css';

interface IResultsListProps {
	names: IBabyName[];
	headerText: string;
}

function ResultsList({ names, headerText }: IResultsListProps) {
	const [resultsCopied, setResultsCopied] = useState<boolean>(false);

	const copyToClipboard = () => {
		const list = names.map((n) => n.name).join('\n');
		navigator.clipboard.writeText(list).then(() => setResultsCopied(true));
	};

	if (names.length === 0) {
		return <h2>No results</h2>;
	}

	return (
		<div className={styles.root}>
			<div className={styles.header}>
				<h2>{headerText}</h2>
				<div className={styles.copy} onClick={copyToClipboard}>
					{resultsCopied ? (
						<span>Copied!</span>
					) : (
						<>
							<span>Copy</span>
							<FontAwesomeIcon icon={solid('clipboard')} />
						</>
					)}
				</div>
			</div>
			{names.map((name) => (
				<ResultRow key={name.id} babyName={name} />
			))}
		</div>
	);
}

export default ResultsList;
