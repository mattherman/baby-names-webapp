import React, { useState } from 'react';
import FormButton from '~/components/FormButton';
import styles from './CompareResultsForm.css';

export interface ICompareResultsFormProps {
	onSubmit(emailAddress: string): void;
}

function CompareResultsForm({ onSubmit }: ICompareResultsFormProps) {
	const [emailAddress, setEmailAddress] = useState<string>(null);

	const handleEmailAddressChanged = (
		event: React.ChangeEvent<HTMLInputElement>
	) => {
		setEmailAddress(event.target.value);
	};

	const handleCompareButtonClicked = (e: React.FormEvent) => {
		e.preventDefault();
		onSubmit(emailAddress);
	};

	return (
		<form onSubmit={handleCompareButtonClicked} className={styles.root}>
			<input
				className={styles.emailInput}
				type="text"
				placeholder="Email"
				value={emailAddress ?? ''}
				onChange={handleEmailAddressChanged}
			/>
			<FormButton>Compare Results</FormButton>
		</form>
	);
}

export default CompareResultsForm;
