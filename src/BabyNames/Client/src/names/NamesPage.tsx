import React, { useEffect } from 'react';
import { bindActionCreators } from 'redux';
import namesSlice from './names.slice';
import { NameGender } from '~/models';
import { useAppDispatch, useAppSelector } from '~/redux';

function NamesPage() {
	const dispatch = useAppDispatch();
	const { getRemainingBabyNames } = bindActionCreators(
		namesSlice.actions,
		dispatch
	);
	useEffect(() => {
		getRemainingBabyNames(NameGender.Male);
	}, []);
	const remainingNames = useAppSelector((state) => state.names.remainingNames);
	return (
		<>
			<div>Names</div>
			<ul>
				{remainingNames.map((name) => (
					<li key={name.id}>{name.name}</li>
				))}
			</ul>
		</>
	);
}

export default NamesPage;
