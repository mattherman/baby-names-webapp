import React from 'react';
import { IBabyName, NameGender } from '~/models';
import cx from 'classnames';
import styles from './NameCard.css';

export interface INameCardProps {
	name: IBabyName;
}

export function NameCard({ name }: INameCardProps) {
	const colorStyle = name.gender == NameGender.Male ? styles.blue : styles.pink;
	return <div className={cx(styles.root, colorStyle)}>{name.name}</div>;
}
