import { useEffect, useState } from 'react';
import GeneralAxiosService from '../../services/GeneralAxiosService';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import LeagueTableTeam from '../LeagueComponents/LeagueTableTeam';
import Loading from '../Loading';

const MatchTeamsLeagueTable = (props) => {
    const [leagueTeams, setLeagueTeams] = useState({})
    const [loading, setLoading] = useState(true)

    const league = props.league
    const homeTeam = props.homeTeam
    const awayTeam = props.awayTeam

    useEffect(() => {
        GeneralAxiosService.getMethod('https://localhost:7067/api/v1/Leagues/'+league.leagueId+'/Teams/0')
        .then((response) => 
        setLeagueTeams(
            response.data.pageResults.sort((a, b) => a.name.localeCompare(b.name))
            
            .sort((a, b) => 
            (b.currentSeasonStats.goalsFor - b.currentSeasonStats.goalsAgainst)
            -
            (a.currentSeasonStats.goalsFor - a.currentSeasonStats.goalsAgainst)
            )

            .sort((a, b) => 
                b.currentSeasonStats.points - a.currentSeasonStats.points
            )))
        .then(() => setLoading(false))
    },[league])

    return <div>
        {loading && <Loading />}
        {!loading && <Table className='leagueTable tablesContainer container--styled'>
                <TableHead>
                    <TableRow>
                        <TableCell colSpan={4}>
                            <h4>{league.leagueName} standings</h4>
                        </TableCell>
                        <TableCell align="right">
                            <h5>PL</h5>
                        </TableCell>
                        <TableCell className='tableColumnHide' align="right">
                            <h5>W</h5>
                        </TableCell>
                        <TableCell className='tableColumnHide' align="right">
                            <h5>D</h5>
                        </TableCell>
                        <TableCell className='tableColumnHide' align="right">
                            <h5>L</h5>
                        </TableCell>
                        <TableCell align="right">
                            <h5>GD</h5>
                        </TableCell>
                        <TableCell align="right">
                            <h5>PTS</h5>
                        </TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    <LeagueTableTeam 
                        team={leagueTeams[Math.min(
                            leagueTeams.findIndex(t => 
                                t.id === homeTeam.teamId &&
                                t.name === homeTeam.teamName &&
                                t.logo === homeTeam.teamLogo)
                            ,
                            leagueTeams.findIndex(t => 
                                t.id === awayTeam.teamId &&
                                t.name === awayTeam.teamName &&
                                t.logo === awayTeam.teamLogo)
                        )]} 
                        position={1 + Math.min(
                            leagueTeams.findIndex(t => 
                                t.id === homeTeam.teamId &&
                                t.name === homeTeam.teamName &&
                                t.logo === homeTeam.teamLogo)
                            ,
                            leagueTeams.findIndex(t => 
                                t.id === awayTeam.teamId &&
                                t.name === awayTeam.teamName &&
                                t.logo === awayTeam.teamLogo)
                        )}
                     />
                     <TableRow style={{cursor: 'unset'}}>
                        <TableCell style={{textAlign: 'center', padding: "0", paddingBottom: ".5rem"}} colSpan={10}>
                            <p>...</p>
                        </TableCell>
                     </TableRow>
                    <LeagueTableTeam 
                        team={leagueTeams[Math.max(
                            leagueTeams.findIndex(t => 
                                t.id === homeTeam.teamId &&
                                t.name === homeTeam.teamName &&
                                t.logo === homeTeam.teamLogo)
                            ,
                            leagueTeams.findIndex(t => 
                                t.id === awayTeam.teamId &&
                                t.name === awayTeam.teamName &&
                                t.logo === awayTeam.teamLogo)
                        )]} 
                        position={1 + Math.max(
                            leagueTeams.findIndex(t => 
                                t.id === homeTeam.teamId &&
                                t.name === homeTeam.teamName &&
                                t.logo === homeTeam.teamLogo)
                            ,
                            leagueTeams.findIndex(t => 
                                t.id === awayTeam.teamId &&
                                t.name === awayTeam.teamName &&
                                t.logo === awayTeam.teamLogo)
                        )}
                     />
                </TableBody>
            </Table>}
    </div>
}

export default MatchTeamsLeagueTable;
