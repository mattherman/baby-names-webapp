import React, { useEffect } from 'react';
import { bindActionCreators } from 'redux';
import { useDispatch } from 'react-redux';
import namesSlice from './names.slice';
import { useAppSelector } from '~/redux';
import { NameGender } from '~/models';

function NamesPage() {
	const dispatch = useDispatch();
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
