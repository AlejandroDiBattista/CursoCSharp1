class Hash
	def contar(texto)
		texto.split(//).each{|s| self[s] += 1}
	end
	def total 
		values.sum
	end
	def entropia
		values.map{|f| f/total }.sum{|p| -p * Math.log2(p)}
	end
end


texto = open('.\el_quijote.txt').read
texto = texto.downcase.gsub(/[^a-z]/,"")

contador = Hash.new{0.0}
contador.contar texto

puts "DEMO Entropía"
puts " · Hay #{contador.size} símbolos con una entropía de #{contador.entropia}"

#t = contador.total
#pp contador.sort_by{|k,v|-v}.map{|k,v|[k,(100.0*v/t).round(2)]}