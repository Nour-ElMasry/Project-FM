import { TextField } from '@mui/material';
import MenuItem from '@mui/material/MenuItem';
import { useState } from 'react';

const ListSelect = (props) => {

    const [itemSelected, setItemSelected] = useState(0);

    const itemSelectedChangeHandler = value => {
        setItemSelected(value.target.value);
    }

    return <TextField
      select
      fullWidth
      value={itemSelected}
      label={props.label}
      error={!!props.errors[props.label.toLowerCase()]}
      helperText={props.errors[props.label.toLowerCase()]?.message}
      {...props.register(props.label.toLowerCase())}
      onChange={itemSelectedChangeHandler}
    >
      <MenuItem value={0}>
        <em>None</em>
      </MenuItem>
      {props.list !== [] && props.list.map((c,i) => {
        return <MenuItem value={c.value} key={i}>{c.label}</MenuItem>
      })}
    </TextField>
}

export default ListSelect;