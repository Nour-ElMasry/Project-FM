import { TextField } from '@mui/material';
import MenuItem from '@mui/material/MenuItem';
import { useState } from 'react';

const ListSelect = (props) => {
    var selectOption = props.NoneSelect ? " " : props.list[0].value
    const [itemSelected, setItemSelected] = useState(selectOption);

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
      {props.NoneSelect && <MenuItem value={" "}>
        <em>None</em>
      </MenuItem>}
      {props.list !== [] && props.list.map((c,i) => {
        return <MenuItem value={c.value} key={i}>{c.label}</MenuItem>
      })}
    </TextField>
}

export default ListSelect;