import { useState } from "react"
import { Input } from "~/components/ui/input"
import { Textarea } from "~/components/ui/textarea"
import { Button } from "~/components/ui/button"
import { Select, SelectTrigger, SelectValue, SelectItem, SelectContent } from "~/components/ui/select"
import { Label } from "~/components/ui/label"
import SubExerciseEditor from "./sub-exercise-editor"

export default function ExerciseForm() {
  const [difficulty, setDifficulty] = useState("Unassigned")

  return (
    <form className="space-y-6 p-6 max-w-4xl mx-auto">
      <div>		
        <Label htmlFor="title">Titel</Label>
        <Input id="title" placeholder="Indtast opgavens titel" />
      </div>

      <div>
        <Label htmlFor="summary">Resume</Label>
        <Textarea id="summary" placeholder="Kort beskrivelse af opgaven" />
      </div>

      <div>
        <Label htmlFor="difficulty">Sværhedsgrad</Label>
        <Select value={difficulty} onValueChange={setDifficulty}>
          <SelectTrigger id="difficulty">
            <SelectValue placeholder="Vælg sværhedsgrad" />
          </SelectTrigger>
          <SelectContent>
            <SelectItem value="Unassigned">Ikke valgt</SelectItem>
            <SelectItem value="Easy">Let</SelectItem>
            <SelectItem value="Medium">Middel</SelectItem>
            <SelectItem value="Hard">Svær</SelectItem>
          </SelectContent>
        </Select>
      </div>

      {/* Du kan udskifte disse med faktisk data fra backend */}
      <div>
        <Label>Kategorier</Label>
        <Input placeholder="Fx: Matematik, Programmering" />
      </div>

      <div>
        <Label>Grupper</Label>
        <Input placeholder="Fx: Gruppe A, Gruppe B" />
      </div>

      <SubExerciseEditor />

      <Button type="submit">Gem opgave</Button>
    </form>
  )
}