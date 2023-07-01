function atm(start1, start2, threshold) {
  if (threshold <= 0)
    return start1

  var isEven = false

  for (var i = 0; i < threshold; i++)
  {
    isEven = i % 2 == 0

    if (isEven)
      start1 += start2
    else
      start2 *= start1
  }

  return isEven ? start1 : start2
}