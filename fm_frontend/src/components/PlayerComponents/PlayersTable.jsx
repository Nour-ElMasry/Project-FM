import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import RatingTableCell from '../RatingTableCell';
import CountryTableItem from '../CountryTableItem';
import Pagination from '../Pagination';
import Tooltip from '@mui/material/Tooltip';
import IconButton from '@mui/material/IconButton';
import Toolbar from '@mui/material/Toolbar';
import FilterListIcon from '@mui/icons-material/FilterList';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import DialogTitle from '@mui/material/DialogTitle';
import { Button, TextField } from '@mui/material';
import { useMemo, useState } from 'react';
import { useForm } from 'react-hook-form';
import countryList from 'react-select-country-list';
import MenuItem from '@mui/material/MenuItem';

const PlayersTable = (props) => {
    const players = props.players;
    const isPlayersPage = props.playersPage;
    const { register, handleSubmit, formState: { errors }, reset } = useForm();
    const options = useMemo(() => countryList().getData(), []);
    const [country, setCountries] = useState(" ");

    const countryChangeHandler = value => {
        setCountries(value.target.value);
    }

    const [position, setPosition] = useState(" ");

    const positionChangeHandler = value => {
        setPosition(value.target.value);
    }

    const [openFilter, setOpenFilter] = useState(false);
    
    const handleClickOpen = () => {
        setOpenFilter(true);
    };
  
    const handleClose = () => {
        setOpenFilter(false);
        reset();
        setCountries(" ")
        setPosition(" ");
    };


    const getAge = (dateString) => {
        var today = new Date();
        var birthDate = new Date(dateString);
        var age = today.getFullYear() - birthDate.getFullYear();
        var m = today.getMonth() - birthDate.getMonth();
        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) 
        {
            age--;
        }
        return age;
    }
    
    const dateLongFormat = (date) => {
      var options = { year: 'numeric', month: 'short', day: 'numeric' };
      var today  = new Date(date);

      return today.toLocaleDateString("en-US", options);
    }

    return <div className='container--styledDarker'>
    <Dialog className='dialogContainer' open={openFilter} onClose={handleClose} fullWidth>
      <DialogTitle>Filter Players list</DialogTitle>
      <DialogContent>
      <Box component="form" onSubmit={handleSubmit(props.handleFilterSubmit)} sx={{ mt: 3 }}>
          <Grid container spacing={2}>
              <Grid item xs={12}>
                  <TextField
                      name="name"
                      fullWidth
                      id="name"
                      label="Player Name"
                      error={!!errors['name']}
                      helperText={errors['name']?.message}
                      {...register('name')}
                  />
              </Grid>
              <Grid item xs={12} sm={6}>  
                  <TextField
                    select
                    fullWidth
                    value={country}
                    label="Countries"
                    error={!!errors['country']}
                    helperText={errors['country']?.message}
                    {...register('country')}
                    onChange={countryChangeHandler}
                  >
                    <MenuItem value=" ">
                      <em>None</em>
                    </MenuItem>
                    {options.map((c,i) => {
                      return <MenuItem value={c.label} key={i}>{c.label}</MenuItem>
                    })}
                  </TextField>
                </Grid>

                <Grid item xs={12} sm={6}>  
                  <TextField
                    select
                    fullWidth
                    value={position}
                    label="Position"
                    error={!!errors['position']}
                    helperText={errors['position']?.message}
                    {...register('position')}
                    onChange={positionChangeHandler}
                  >
                    <MenuItem value=" ">
                      <em>None</em>
                    </MenuItem>
                    <MenuItem value="Attacker">Attacker</MenuItem>
                    <MenuItem value="Midfielder">Midfielder</MenuItem>
                    <MenuItem value="Defender">Defender</MenuItem>
                    <MenuItem value="Goalkeeper">Goalkeeper</MenuItem>
                  </TextField>
                </Grid>
                <Grid item xs={6} sm={6}>  
                    <TextField
                        label="Min age"
                        type="number"
                        error={!!errors['minYearOfBirth']}
                        helperText={errors['minYearOfBirth']?.message}
                        {...register('minYearOfBirth')}
                    />
                </Grid>
                <Grid item xs={6} sm={6}>  
                    <TextField
                        label="Max age"
                        type="number"
                        error={!!errors['maxYearOfBirth']}
                        helperText={errors['maxYearOfBirth']?.message}
                        {...register('maxYearOfBirth')}
                    />
                </Grid>
          </Grid>
      </Box>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleClose}>Cancel</Button>
        <Button onClick={handleClose}>Apply</Button>
      </DialogActions>
    </Dialog>
    <Toolbar className='flex flex-ai-c flex-jc-sb'>
        <h4>Players List</h4>
        <Tooltip title="Filter list">
            <IconButton onClick={handleClickOpen}>
                <FilterListIcon />
            </IconButton>
        </Tooltip>
    </Toolbar>
    <TableContainer className="tablesContainer">
      <Table className="teamPlayersTable" sx={{ minWidth: 500 }} aria-label="simple table">
        <TableHead>
          <TableRow sx={{cursor: "default"}}>
            <TableCell><h4>Player Info</h4></TableCell>
            {isPlayersPage && <>
              <TableCell className='tableColumnHide' align="center"><h4>Team</h4></TableCell>
              <TableCell className='tableColumnHide' align="center"><h4>Attacking</h4></TableCell>
              <TableCell className='tableColumnHide' align="center"><h4>PlayMaking</h4></TableCell>
              <TableCell className='tableColumnHide' align="center"><h4>Defending</h4></TableCell>
              <TableCell className='tableColumnHide' align="center"><h4>GoalKeeping</h4></TableCell>
            </>}
            <TableCell align="center"><h4>Country</h4></TableCell>
            <TableCell align="center"><h4>Birth Date (Age)</h4></TableCell> 
          </TableRow>
        </TableHead>
        <TableBody>
          {players?.pageResults?.sort((a, b) => a.playerPerson.name.localeCompare(b.playerPerson.name)).map((p) => (
            <TableRow
              key={p.id}
              sx={{ '&:last-child td, &:last-child th': { border: 0 }, cursor: "default"}}
            >
              <TableCell component="th" scope="row">
                <div className="flex flex-ai-c">
                    <img className="playerImg" src={p.playerPerson?.image} alt="person"/>
                    <div>
                        <h4 className="teamPlayersTablePlayerName">{p.playerPerson?.name}</h4>
                        <p className="teamPlayersTablePlayerPosition">{p.position}</p>
                    </div>
                </div>
              </TableCell>
              {isPlayersPage && <>
              <TableCell className='tableColumnHide' align="center">
              <img className="teamLogo" src={p.currentTeam?.teamLogo} alt="person"/></TableCell>
              <RatingTableCell rating={p.playerStats?.attacking}/>
              <RatingTableCell rating={p.playerStats?.playMaking}/>
              <RatingTableCell rating={p.playerStats?.defending}/>
              <RatingTableCell rating={p.playerStats?.goalkeeping}/>
            </>}
              <CountryTableItem country={p.playerPerson?.country}/>
              <TableCell align="center">{dateLongFormat(p.playerPerson?.birthDate)} ({getAge(p.playerPerson.birthDate)})</TableCell>
            </TableRow>
          ))}
          <TableRow>
            <TableCell colSpan={4} >
              <Pagination page={players?.currentPage} maxPages={players?.totalPages} pageLoading={props.loading} handlePageChange={props.handlePageChange}/>
            </TableCell>
          </TableRow>
        </TableBody>
      </Table>
    </TableContainer>
  </div>
}

export default PlayersTable;